using Abstraction.Interfaces;
using Domain.Dtos.Read;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineExamWeb.Controllers
{
    public class QuizController : Controller
    {
        private readonly IConsumer _consumer;

        public QuizController(IConsumer consumer)
        {
            _consumer = consumer;
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                _ = Task.Run(async () =>
                {
                    //await Task.Delay(1000); // 60 seconds delay

                    var consumeModel = new ConsumeModel<QuizMessage>(
                        queueTag: "quiz-queue",
                        action: async (message) =>
                        {

                            Console.WriteLine($"Received quiz message: {message.Question}");
                            await Task.CompletedTask;
                            return true;
                        },
                        setting: new ConsumeSettingModel
                        {
                            Durable = true,
                            Exclusive = false,
                            AutoDelete = false,
                            AutoAck = false,
                            Multiple = false,
                            ReQueue = true,
                            PreFetchSize = 0,
                            PreFetchCount = 1,
                            Global = false
                        }
                    );

                    await _consumer.ReceiveAsync(consumeModel);
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return View();
        }

    }
}
