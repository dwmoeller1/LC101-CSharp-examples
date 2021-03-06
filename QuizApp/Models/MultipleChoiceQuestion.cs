﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models
{
    public class MultipleChoiceQuestion : Question
    {
        private List<Answer> possibleAnswers = new List<Answer>();

        public override string Text { get; set; }

        public override List<Answer> SelectedAnswers { get; set; }

        public override IReadOnlyCollection<Answer> PossibleAnswers
        {
            get { return possibleAnswers.AsReadOnly(); }
            protected set { possibleAnswers = (List<Answer>)value; }
        }

        public override void AddAnswer(Answer answer)
        {
            if (possibleAnswers.Any(ans => ans.IsCorrectAnswer) && answer.IsCorrectAnswer)
                throw new Exception("This question can have only one correct answer selected.");

            possibleAnswers.Add(answer);
        }

        public override bool GradeQuestion()
        {
            Answer correctAnswer = possibleAnswers.Single(ans => ans.IsCorrectAnswer);
            return SelectedAnswers.Single().Equals(correctAnswer);
        }

        public override void RemoveAnswer(Answer answer)
        {
            Answer answerToRemove = possibleAnswers.SingleOrDefault(ans => ans.Equals(answer));
            if (answerToRemove != null)
                possibleAnswers.Remove(answerToRemove);
        }

        public void SetCorrectAnswer(int answerId)
        {
            possibleAnswers.ForEach(ans => ans.IsCorrectAnswer = (ans.Id == answerId));
        }
    }
}
