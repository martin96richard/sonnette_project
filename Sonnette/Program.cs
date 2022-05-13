using System.Device.Gpio;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace Sonnette;

public class Program
{
    private const int LED_PIN = 10;
    private const int BTN_PIN = 12;

    public static void Main(string[] args)
    {
        bool prevState = false;
        bool state = false;
        var client = new HttpClient();
        var uri = new Uri("https://192.168.71.51:5002/Notifications");
        //var content = "{\"id\": 1,\"date\": \"2022-03-18T16:20:05.8166092+01:00\",\"typeAppui\": 1}";
        Notification notif = new Notification();
        notif.id = 1;   
        notif.date = DateTime.Now;
        notif.type = 1;

        GpioController controller = new GpioController(PinNumberingScheme.Board);
        controller.OpenPin(LED_PIN, PinMode.Output);
        controller.OpenPin(BTN_PIN, PinMode.Input);

        Console.WriteLine("V4");
        controller.Write(LED_PIN, PinValue.Low);
        
        while (true)
        {
            PinValue btn = controller.Read(BTN_PIN);
            state = btn.Equals(PinValue.Low);

            if (state != prevState)
            {
                if (state)
                {
                    controller.Write(LED_PIN, PinValue.High);
                    Console.Write("ON");
                    client.PostAsync(uri, new StringContent((JsonConvert.SerializeObject(notif))));
                }
                else
                {
                    controller.Write(LED_PIN, PinValue.Low);
                    Console.Write("OFF");
                }
                prevState = state;
            }
        }
    }
}
