using static System.Console;

namespace MethodsPrint
{
    public class ClassPrint
    {
        public static void PrintSubject(string code, string name, string adminDistrict, double population, double square, string districtName, string codeDist)
        {
            WriteLine("\t Код: {0}. \t Наименование: {1}.", code, name);
            WriteLine("Административный центр: {0}.", adminDistrict);
            WriteLine("Население: {0} тыс. чел. \t Площадь: {1} кв. м.", population, square);
            WriteLine("Федеральный округ: {0}. \t Код федерального округа: {1}", districtName, codeDist);
        }
        public static void PrintPopulationDensity(double federalPopulatiion, double federalSquare, double subjectPopulation, double subjectSquare)
        {
            WriteLine("Плотность населения(текущего субъекта): {0} тыс. чел./ кв. м.", subjectPopulation/subjectSquare);
            WriteLine("Плотность начеления(текущего федерального округа): {0} тыс. чел./ кв. м.", federalPopulatiion/federalSquare);
        }
    }
}
