using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SeviceAbstraction;
using Shared.DTOs.Basket;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentsController(IServicesManager servicesManager) : ControllerBase
{
    [HttpPost("{basketId}")]
    public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
    {
        var basket = await servicesManager.PaymentService.CreateOrUpdatePaymentAsync(basketId);
        return Ok(basket);
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
        using var reader = new StreamReader(Request.Body);
        var json = await reader.ReadToEndAsync();

        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        if (!root.TryGetProperty("type", out var typeElement) ||
            !root.TryGetProperty("data", out var dataElement) ||
            !dataElement.TryGetProperty("object", out var objectElement) ||
            !objectElement.TryGetProperty("id", out var idElement))
        {
            return BadRequest();
        }

        var eventType = typeElement.GetString();
        var paymentIntentId = idElement.GetString();
        if (string.IsNullOrWhiteSpace(paymentIntentId))
            return BadRequest();

        if (eventType is "payment_intent.succeeded" or "payment_intent.payment_failed")
        {
            await servicesManager.PaymentService.UpdateOrderPaymentStatusAsync(
                paymentIntentId,
                eventType == "payment_intent.succeeded");
        }

        return Ok();
    }
}
