using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace async
{
    internal class in_out_ref_params
    {
        
        private void Out(out int numero)
        {
            // retornar o valor dentro da função para fora com out
            numero = 123;
        }

        private void Ref(ref int adicao)
        {
            //valor vai ser 2, pois ele adiciona na referência, não faz a cópia do numero para a função
            adicao++; 
        }

        private void In(in int number)
        {
            //não permite alterar o valor passado por referência
            //number++;
        }

        private void Params(params int[] lista)
        {
            //com o params não tem inicio e fim
            //valores infinos
            foreach (var item in lista)
            {
                Console.WriteLine(item);
            }
        }

        public void Execute()
        {
            int valor;
            Out(out valor);
            Console.WriteLine(valor); 
            int adicao = 1;
            Ref(ref adicao);
            Console.WriteLine(adicao);

            Params(1, 5, 10);
        }

    }
}
