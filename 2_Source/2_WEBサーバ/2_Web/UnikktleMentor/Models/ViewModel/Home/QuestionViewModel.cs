using System.Collections.Generic;
using System.Runtime.Serialization;
using UnikktleMentorEngine;

namespace UnikktleMentor.Models
{
    [DataContract]
    public class QuestionViewModel
    {
        [DataMember(Name = "l")]
        public List<Question> QuestionList { get; set; }

    }
}
