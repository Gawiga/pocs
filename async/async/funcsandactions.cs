using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace async
{
    public class funcsandactions
    {
        static async Task read()
        {
            var lines = File.ReadAllLinesAsync("myfile.txt");

            foreach (var item in await lines)
            {
                Console.WriteLine(item);
            }
        }

        Func<Task> readAll = async () =>
        {
            var lines = File.ReadAllLinesAsync("myfile.txt");

            foreach (var item in await lines)
            {
                Console.WriteLine(item);
            }

        };

        Action<Task> readAction = async a =>
        {
            var lines = File.ReadAllLinesAsync("myfile.txt");

            foreach (var item in await lines)
            {
                Console.WriteLine(item);
            }
        };


        public Action<Task<string>> readAct = async a =>
        {
            Console.WriteLine(await a);
        };


        //readAct(ola());

        async Task<string> ola()
        {
            return "hello";
        }


    }
}
