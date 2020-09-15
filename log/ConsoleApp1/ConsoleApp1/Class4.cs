using Microsoft.Extensions.DiagnosticAdapter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConsoleApp1
{
    public static class Class4
    {
        public class Observer<T> : IObserver<T>
        {
            private readonly Action<T> _onNext;
            public Observer(Action<T> onNext)
            {
                _onNext = onNext;
            }
            public void OnCompleted()
            {
              
            }

            public void OnError(Exception error)
            {
                
            }

            public void OnNext(T value)
            {
                _onNext(value);
            }
        }

        public class CustomSourceCollector
        {
            [DiagnosticName("Hello")]
            public void OnHello(int type, string msg)
            {
                Console.WriteLine($"Type:{type}");
                Console.WriteLine($"Msg:{msg}");
            }
        }

        public static void Run()
        {
            //DiagnosticListener.AllListeners.Subscribe(new Observer<DiagnosticListener>(listener =>
            //{
            //    if (listener.Name=="AppLog")
            //    {
            //        listener.Subscribe(new Observer<KeyValuePair<string, dynamic>>(eventData =>
            //         {
            //             var (key, value) = eventData;
            //             Console.WriteLine($"Name:{key}");
            //             Console.WriteLine($"value:{value}");
            //         }));
            //    }
            //}));

            var source = new DiagnosticListener("AppLog");

            //source.Subscribe(new Observer<KeyValuePair<string, dynamic>>(eventData =>
            //{
            //    var (key, value) = eventData;
            //    Console.WriteLine($"Name:{key}");
            //    Console.WriteLine($"Value:{value}");
            //}));

            source.SubscribeWithAdapter(new CustomSourceCollector());

            source.Write("Hello", new
            {
                Type = 1,
                Msg = "20200722"
            });
        }
    }
}
