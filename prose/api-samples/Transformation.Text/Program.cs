// See https://aka.ms/new-console-template for more information


using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Text;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

LearnFornatName();
LearnNormalizePhoneNumber();

static void LearnFornatName()
{
    IEnumerable<Constraint<IRow, object>> constraints = new[]
    {
        new Example(new InputRow("Phan Tan Tai"), "Tai Phan Tan")
    };

    Program topRankedProgram = Learner.Instance.Learn(constraints);

    if (topRankedProgram == null)
    {
        Console.Error.WriteLine("Error: failed to learn format name program");
    }
    else
    {
        // Run the program on some new inputs.
        foreach (var name in new[] { "Etelka Bala", "Myron Lampros" })
        {
            string formatted = topRankedProgram.Run(new InputRow(name)) as string;
            Console.WriteLine("\"{0}\" => \"{1}\"", name, formatted);
        }
    }
}

static void LearnNormalizePhoneNumber()
{
    // Some programs may require multiple examples.
    // More examples ensures the proper program is learned and may speed up learning.
    IEnumerable<Constraint<IRow, object>> constraints = new[]
    {
                new Example(new InputRow("425-829-5512"), "425-829-5512"),
                new Example(new InputRow("(425) 829 5512"), "425-829-5512")
            };
    Program topRankedProgram = Learner.Instance.Learn(constraints);

    if (topRankedProgram == null)
    {
        Console.Error.WriteLine("Error: failed to learn normalize phone number program.");
    }
    else
    {
        foreach (var phoneNumber in new[] { "425 233 1234", "(425) 777 3333" })
        {
            string normalized = topRankedProgram.Run(new InputRow(phoneNumber)) as string;
            Console.WriteLine("\"{0}\" => \"{1}\"", phoneNumber, normalized);
        }
    }
}
