using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace excel
{
    public static class UnitTest
    {
        private static void Check(dynamic x, dynamic y, dynamic z) // метод який перевірятиме чи правильно працює Юніт тест, якщо ні то повертає номер тесту який не пройшов перевірку
        {
            if (x != y)
            {
                throw new Exception("Юніт тест №" + z + " не пройдено");
            }
        }
        public static void UnitTests()
        {
            Check(Parser.parse("1-1"), 0, 1); // (1-1 = 0)
            Check(Parser.parse("3+7     *   4"), 31, 2); // (перевірка на видалення пробілів)
            Check(Parser.parse("1 = 0 and 1 > 0"), false, 3); //перевірка розпізнавання логічних операцій
            Check(Parser.parse("not(1 < 0)"), true, 4); // перевірка розпізнавання інших логічних операцій
            Check(Parser.parse("(2^0) > 1"), false, 5); // перевірка сумісності арифметичних та логічних операцій
            Check(Parser.parse("max(-6,5,3) = min(5,6,7) "), true, 6); // перевірка розпізнавання та роботи функцій макс та мін
            Check(Parser.parse("3.4 * 5"), 17, 7); // перевірка для не цілих чисел
            Check(Parser.find_bracket("max(3,4,5,6,7,8,9,10,11,12,13,14,15,16,17)", 3), 41, 8);
            Check(Parser.find_bracket("[(x+3)]", 0), 6, 9);
            Check(Parser.find_bracket("3^7*((3-5*(2-2)))", 4), 16, 10);
            Check(Parser.findleft("3+7     *   4", '4'),12 , 11);
            Check(Parser.findleft("788*3", '+'), -1, 12);
            Check(Parser.findright("3^7*((3-5*(2-2)))", ')'), 16, 13);
            Check(Parser.findright("18*5*3", '*'), 4, 14);
        }
    };
}
