using System;
using System.Collections.Generic;
using System.Text;
using Conzo;
using Conzo.Commands;
using Conzo.Configuration;

namespace Example
{
   public class Quiz
   {
      public static void Run()
      {
         try
         {
            var controller = new QuizController();

            var welcome = new Command(controller.GetWelcome);
            var question = new Command(controller.GetQuestion);
            var answerA = new Command(controller.AnsweredA, controller.AnyQuestionsLeft);
            var answerB = new Command(controller.AnsweredB, controller.AnyQuestionsLeft);
            var answerC = new Command(controller.AnsweredC, controller.AnyQuestionsLeft);
            var outro = new Command(controller.GetOutro);

            var settings = new Settings(welcome)
            {
               QuitDelay = 2000
            };

            var myApp = ConsoleApplication.Create(settings);

            myApp.Configure(welcome)
               .AddNextCommand(ConsoleKey.Enter, question);

            myApp.Configure(question)
               .AddNextCommand(ConsoleKey.A, answerA)
               .AddNextCommand(ConsoleKey.B, answerB)
               .AddNextCommand(ConsoleKey.C, answerC);

            myApp.Configure(answerA)
               .AddNextCommand(ConsoleKey.Enter, question);

            myApp.Configure(answerB)
               .AddNextCommand(ConsoleKey.Enter, question);

            myApp.Configure(answerC)
               .AddNextCommand(ConsoleKey.Enter, question);

            myApp.AddGlobalCommand(settings.QuitKey, outro);

            myApp.Run();
         }
         catch (Exception exception)
         {
            Console.WriteLine(exception);
         }
      }
   }

   class QuizController
   {
      private int _currentQuestionIndex;
      private readonly QuizRepository _quizRepository;
      private int _correctAnswers;

      public QuizController()
      {
         _quizRepository = new QuizRepository();
      }

      public string GetWelcome()
      {
         return "Welcome!" + Environment.NewLine + "Press ENTER to start the quiz...";
      }

      public string GetOutro()
      {
         return "Goodbye...";
      }

      public string GetQuestion()
      {
         var question = GetCurrentQuestion();

         string textToDisplay = string.Format("You finished the quiz with {0} correct answers!", _correctAnswers);
         if (question != null)
         {
            textToDisplay = Format(question);
         }

         return textToDisplay;
      }

      public bool AnyQuestionsLeft()
      {
         return _currentQuestionIndex <= (_quizRepository.GetAll().Count - 1);
      }

      private string CheckAnswer(int answerIndex)
      {
         var q = GetCurrentQuestion();

         string textToShow = "That is NOT correct...";
         if (answerIndex == q.Answer)
         {
            textToShow = "Correct!";
            _correctAnswers++;
         }

         textToShow += Environment.NewLine + Environment.NewLine + "Press ENTER to continue...";

         // Let's move on to the next question.
         _currentQuestionIndex++;

         return textToShow;
      }

      private QuestionAndAnswer GetCurrentQuestion()
      {
         QuestionAndAnswer question = null;

         if (AnyQuestionsLeft())
         {
            question = _quizRepository.GetAll()[_currentQuestionIndex];
         }

         return question;
      }

      public string AnsweredA()
      {
         return CheckAnswer(0);
      }

      public string AnsweredB()
      {
         return CheckAnswer(1);
      }

      public string AnsweredC()
      {
         return CheckAnswer(2);
      }

      private string Format(QuestionAndAnswer questionAndAnswer)
      {
         var stringBuilder = new StringBuilder();

         stringBuilder.AppendFormat("{0}{1}", questionAndAnswer.Question, Environment.NewLine);
         stringBuilder.AppendFormat("A: {0}{1}", questionAndAnswer.Choices[0], Environment.NewLine);
         stringBuilder.AppendFormat("B: {0}{1}", questionAndAnswer.Choices[1], Environment.NewLine);
         stringBuilder.AppendFormat("C: {0}{1}", questionAndAnswer.Choices[2], Environment.NewLine);

         return stringBuilder.ToString();
      }
   }

   class QuestionAndAnswer
   {
      public string Question { get; set; }
      public List<string> Choices { get; set; }
      public int Answer { get; set; }
   }

   class QuizRepository
   {
      private static readonly List<QuestionAndAnswer> Questions = new List<QuestionAndAnswer>
      {
         new QuestionAndAnswer
         {
            Question = "What is the capital of The Netherlands?",
            Choices = new List<string> {"Amsterdam", "Rotterdam", "The Hague"},
            Answer = 0
         },
         new QuestionAndAnswer
         {
            Question = "1 + 1 =",
            Choices = new List<string> {"0", "1", "2"},
            Answer = 2
         },
         new QuestionAndAnswer
         {
            Question = "What is the first letter of the alphabet?",
            Choices = new List<string> {"A", "B", "C"},
            Answer = 0
         }
      };

      public List<QuestionAndAnswer> GetAll()
      {
         return Questions;
      }
   }
}
