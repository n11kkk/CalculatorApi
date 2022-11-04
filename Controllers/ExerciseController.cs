using Microsoft.AspNetCore.Mvc;

namespace CalApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExerciseController : ControllerBase
    {
        private static int[] Nums1 = {Random.Shared.Next(1, 99), Random.Shared.Next(1, 99), Random.Shared.Next(1, 99), Random.Shared.Next(1, 99), Random.Shared.Next(1, 99), Random.Shared.Next(1, 99), Random.Shared.Next(1, 99), Random.Shared.Next(1, 99), Random.Shared.Next(1, 99), Random.Shared.Next(1, 99)
    };
        private static int[] Nums2 = new int[10];

        private int[] Ans = new int[10];

        private static readonly string[] Operator = { "+", "-", "*", "/" };

        private string[] OperatorsUsed = new string[10];

        private int[][] options = new int[10][];

        private readonly ILogger<ExerciseController> _logger;

        public ExerciseController(ILogger<ExerciseController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetExercise")]
        public ActionResult<Exercise> Get()
        {
            var randomizing = new Random();
            for (int i = 0; i < 10; i++)
            {
                Nums2[i] = Random.Shared.Next(1,Nums1[i]);
                OperatorsUsed[i] = Operator[Random.Shared.Next(Operator.Length)];

                if (OperatorsUsed[i] == "+") { Ans[i] = Nums1[i] + Nums2[i]; }
                else if (OperatorsUsed[i] == "-") { Ans[i] = Nums1[i] - Nums2[i]; }
                else if (OperatorsUsed[i] == "*") { Ans[i] = Nums1[i] * Nums2[i]; }
                else { Ans[i] = Nums1[i] / Nums2[i]; }

                if (Ans[i] > 0)
                {
                    options[i] = new int[] { Random.Shared.Next(1, Ans[i]+10), Random.Shared.Next(1, Ans[i]+10), Ans[i] };
                    randomizing.Randomise(options[i]);
                }
                else
                {
                    options[i] = new int[] { Random.Shared.Next(Ans[i], 1), Random.Shared.Next(Ans[i], 1), Ans[i] };
                    randomizing.Randomise(options[i]);
                }

            }
            return new Exercise
            {
                FirstNum = Nums1,
                SecondNum = Nums2,
                Operators = OperatorsUsed,
                Answers = Ans,
                Options = options,
            };
        }
    }
}

static class RandomE
{
    public static void Randomise<T>(this Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
