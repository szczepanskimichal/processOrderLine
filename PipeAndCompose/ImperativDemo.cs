using System.Text.Json;

namespace ConsoleApp1;

public record OrderLineInput(
    int ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity
);

public record OrderLine(
    int ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity
);

public record PricedOrderLine(
    int ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity,
    decimal Total
);

public class ImperativDemo
{
    public static void Run()
    {
        var json = """
                   {
                       "productId": 10,
                       "productName": "Coffee",
                       "unitPrice": 39,
                       "quantity": 3
                   }
                   """;
        var input = ParseOrderLine(json);
        var validated = ValidateInput(input);
        var orderLine = CreateOrderLine(validated);
        var priced = CalculateTotal(orderLine);
        var output = FormatOrderLine(priced);
        Console.WriteLine(output);

    }

    static OrderLineInput ParseOrderLine(string json)
    {
        return JsonSerializer.Deserialize<OrderLineInput>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
    }

    static OrderLineInput ValidateInput(OrderLineInput input)
    {
        // Happy path nå: vi later som alt er gyldig.
        // Senere kan denne returnere Result<OrderLineInput>.
        return input;
    }

    static OrderLine CreateOrderLine(OrderLineInput input)
    {
        return new OrderLine(
            ProductId: input.ProductId,
            ProductName: input.ProductName.Trim(),
            UnitPrice: input.UnitPrice,
            Quantity: input.Quantity);
    }

    static PricedOrderLine CalculateTotal(OrderLine orderLine)
    {
        return new PricedOrderLine(
            ProductId: orderLine.ProductId,
            ProductName: orderLine.ProductName,
            UnitPrice: orderLine.UnitPrice,
            Quantity: orderLine.Quantity,
            Total: orderLine.UnitPrice * orderLine.Quantity);
    }

    static string FormatOrderLine(PricedOrderLine line)
    {
        return $"{line.Quantity} x {line.ProductName} = {line.Total}";
    }
}