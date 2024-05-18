namespace UnikktleMentor.Models
{
    public class C02系統値_TeamUser
    {
        public long Id { get; set; }            // [UnikktleMentorWeb].[hst].[tC02系統値].[UserNo]
        public string Nickname { get; set; }    // [UnikktleMentorWeb].[usr].[tUserSetting].[Nickname]
        public byte 抑うつ性 { get; set; }
        public byte 気分の変化 { get; set; }
        public byte 劣等感 { get; set; }
        public byte 神経質 { get; set; }
        public byte 主観性 { get; set; }
        public byte 協調性 { get; set; }
        public byte 攻撃性 { get; set; }
        public byte 活動性 { get; set; }
        public byte のん気 { get; set; }
        public byte 思考性 { get; set; }
        public byte 支配性 { get; set; }
        public byte 社会性 { get; set; }
    }

}
