using ProyectoModelado2024.Server.Repositorio;
using ProyectoModelado2024.Shared.DTO;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ProyectoModelado2024.Server.Servicios
{
    public class WhatsAppService : IWhatsAppService
    {
        private readonly IConfiguration _configuration;
        private readonly IPdfService _pdfService;
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<WhatsAppService> _logger;

        public WhatsAppService(
            IConfiguration configuration,
            IPdfService pdfService,
            IPedidoRepositorio pedidoRepositorio,
            IWebHostEnvironment env,
            ILogger<WhatsAppService> logger)
        {
            _configuration = configuration;
            _pdfService = pdfService;
            _pedidoRepositorio = pedidoRepositorio;
            _env = env;
            _logger = logger;

            // Inicializar Twilio con las credenciales del appsettings.json
            var accountSid = _configuration["Twilio:AccountSid"];
            var authToken = _configuration["Twilio:AuthToken"];

            if (!string.IsNullOrEmpty(accountSid) && !string.IsNullOrEmpty(authToken))
            {
                TwilioClient.Init(accountSid, authToken);
                _logger.LogInformation("Twilio inicializado correctamente");
            }
            else
            {
                _logger.LogWarning("Twilio no está configurado. Verifica appsettings.json");
            }
        }

        public async Task<bool> EnviarPedidoAsync(CrearPedidoDTO pedidoDTO)
        {
            try
            {
                _logger.LogInformation($"Iniciando envío del pedido");

                // 1. Generar PDF
                var pdfBytes = await _pdfService.GenerarPdfPedidoAsync(pedidoDTO);
                _logger.LogInformation("PDF generado");

                // 2. Obtener número destino
                var numeroDestino = _configuration["Twilio:NumeroWhatsAppDestino"];
                if (string.IsNullOrEmpty(numeroDestino))
                {
                    _logger.LogError("Número destino no configurado");
                    return false;
                }

                // 3. Guardar PDF temporalmente para enviar
                string fileName = $"Pedido_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                string tempPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "temp", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(tempPath));
                await File.WriteAllBytesAsync(tempPath, pdfBytes);

                // 4. Crear URL pública del archivo
                var appUrl = _configuration["AppUrl"] ?? "https://localhost:7189";
                var fileUrl = $"{appUrl}/temp/{fileName}";

                // 5. Preparar mensaje

                var mensaje = $@" *NUEVO PEDIDO* 
                            
                            Adjuntamos el detalle completo.";

                // 6. Enviar mensaje de texto
                await EnviarMensajeTextoAsync(numeroDestino, mensaje);

                // Pequeña pausa entre mensajes
                await Task.Delay(2000);

                // 8. Enviar PDF como documento
                var messageOptions = new CreateMessageOptions(new PhoneNumber(numeroDestino))
                {
                    From = new PhoneNumber(_configuration["Twilio:WhatsAppNumber"]),
                    Body = "📎 Detalle del pedido",
                    MediaUrl = new List<Uri> { new Uri(fileUrl) }
                };

                var message = await MessageResource.CreateAsync(messageOptions);
                _logger.LogInformation($"Mensaje enviado con SID: {message.Sid}");

                File.Delete(tempPath);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error enviando pedido: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EnviarMensajeTextoAsync(string numero, string mensaje)
        {
            try
            {
                var messageOptions = new CreateMessageOptions(new PhoneNumber(numero))
                {
                    From = new PhoneNumber(_configuration["Twilio:WhatsAppNumber"]),
                    Body = mensaje
                };

                var message = await MessageResource.CreateAsync(messageOptions);
                _logger.LogInformation($"Mensaje de texto enviado: {message.Sid}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error enviando mensaje de texto: {ex.Message}");
                return false;
            }
        }
    }
}
