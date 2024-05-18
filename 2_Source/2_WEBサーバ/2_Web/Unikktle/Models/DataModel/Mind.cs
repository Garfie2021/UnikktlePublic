using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unikktle.Common;


namespace Unikktle.Models
{
    // 検索画面用
    [DataContract]
    public class MindSearch
    {
        // プロパティ名は「No」禁止。「Id」じゃないと実行時エラーになる。
        [DataMember(Name = "i")]
        public long Id { get; set; }

        [DataMember(Name = "t")]
        public string Title { get; set; }

        [DataMember(Name = "e")]
        public string Explanation { get; set; }

        [DataMember(Name = "p")]
        public bool PublishOnlyToMe { get; set; }

        [DataMember(Name = "a")]
        public bool AllowOtherEdit { get; set; }

        [DataMember(Name = "l")]
        public DateTime? LastUpdate { get; set; }
    }

    // 編集画面用
    public class Mind
    {
        // プロパティ名は「No」禁止。「Id」じゃないと実行時エラーになる。
        public long Id { get; set; }

        public long UserNo { get; set; }

        public string Title { get; set; }

        public string Explanation { get; set; }

        public string Item_SpaceSeparator { get; set; }

        public string ItemRelation { get; set; }

        public bool AllowOtherEdit { get; set; }

        public bool PublishOnlyToMe { get; set; }
    }

    public class MindJson
    {
        // プロパティ名は「No」禁止。「Id」じゃないと実行時エラーになる。
        public long Id { get; set; }

        public string JsonViewModel { get; set; }
    }

    // 編集画面で入力された値のクラス変換用
    public class MindItemRow
    {
        public ItemType ItemType { get; set; }

        public long KeywordNo { get; set; }

        // 「アイテム間の関係」欄に入力された値の行No。
        // MindRelationRowクラス、MindItemRowクラス、MindRow_Textクラスを紐付けるのに使う。
        public long ItemRelationshipNo { get; set; }
        

        public string Sentence { get; set; }

        public int SentenceWidth { get; set; }
    }

    public class MindRelationRow
    {
        // 「アイテム間の関係」欄に入力された値の行No。
        // MindRelationRowクラス、MindItemRowクラス、MindRow_Textクラスを紐付けるのに使う。
        public long ItemRelationshipNo { get; set; }

        // KeywordNo_Leftに対応するSuperParentNo
        public long? Left_SuperParentNo { get; set; }
        public string Left_SuperParent_Sentence { get; set; }

        public int Left_SuperParent列No { get; set; }

        public long Left_KeywordNo { get; set; }

        public string Left_KeywordSentence { get; set; }

        // KeywordNo_Rightに対応するSuperParentNo
        public long? Right_SuperParentNo { get; set; }
        public string Right_SuperParent_Sentence { get; set; }

        public int Right_SuperParent列No { get; set; }


        public long Right_KeywordNo { get; set; }

        public string Right_KeywordSentence { get; set; }

        public RelationType RelationType { get; set; }

        public LineType LineType { get; set; }
        
        public long KeywordNo_Sentence { get; set; }

        public string Sentence { get; set; }
    }

    [Serializable] // ← DeepCopy用
    public class SuperParent
    {
        public long KeywordNo { get; set; }

        public int X1 { get; set; }

        public int X2 { get; set; }

        public int Y1 { get; set; }

        public int Y2 { get; set; }

        public int SuperParent列No { get; set; }

        public string Sentence { get; set; }
    }


    public class MindRow_Text
    {
        public long KeywordNo { get; set; }

        // 「アイテム間の関係」欄に入力された値の行No。
        // MindRelationRowクラス、MindItemRowクラス、MindRow_Textクラスを紐付けるのに使う。
        public long ItemRelationshipNo { get; set; }

        public string Sentence { get; set; }

        // 最も親に当たるItemのNo。
        public long? SuperParentNo { get; set; }
        public string SuperParent_Sentence { get; set; }

        //// true  : SuperParent
        //// false : 子Item
        //public bool SuperParent { get; set; } = false;

        // 親ItemのNo。
        public long? ParentNo { get; set; }

        // 親子関係の子アイテムが何列目に該当するか。
        public int ChildColumnNo { get; set; }

        // 親子関係の子アイテムが何列目に該当するか。
        public int ChildCount { get; set; } = 0;

        // SVG上で何行目に該当するか。SuperParent単位。
        public int ChildRowNo { get; set; } = 0;

        // 親Itemであれば、子アイテムの数。子アイテムを持って居なければ0。
        public int ChildItemCount { get; set; } = 0;

        //// Z軸の階層No。
        //// SuperParentが1。
        //public int Z_No { get; set; }

        public int X1 { get; set; }

        public int Y1 { get; set; }

        //public int Width { get; set; }

        //public int Height { get; set; }

        // X1 に Width を足した値。
        public int X2 { get; set; }

        // Y1 に Height を足した値。
        public int Y2 { get; set; }

        public int 保持している子アイテムの階層 { get; set; }
        public int 保持している全子アイテムの数 { get; set; }
        

        // Y座標の調整済みフラグ。
        // false : 未調整
        // true  : 調整済み
        public bool Adjusted_Y { get; set; } = false;
    }

    public class MindRow_Rect
    {
        public long KeywordNo { get; set; }

        public string Sentence { get; set; }

        // Z軸の階層No。
        // SuperParentが1。
        public int Z_No { get; set; }

        public int X1 { get; set; }

        public int Y1 { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Fill { get; set; }
        
        //// X1 に Width を足した値。
        //public int X2 { get; set; }

        //// Y1 に Height を足した値。
        //public int Y2 { get; set; }

        // 幅/高さの調整済みフラグ。
        // false : 未調整
        // true  : 調整済み
        //public bool Adjusted { get; set; } = false;
    }

    public class MindRow_Line
    {
        public int Cnt { get; set; }

        public LineType Type { get; set; } = LineType.SuperParentが違う;

        public long No_Left { get; set; }

        public long? SuperParentNo_Left { get; set; }

        public int ItemCount_Left { get; set; }

        public int X1 { get; set; }

        public int Y1 { get; set; }


        public long No_Right { get; set; }

        public long? SuperParentNo_Right { get; set; }

        public int ItemCount_Right { get; set; }

        public string Stroke { get; set; }
        
        public int X2 { get; set; }

        public int Y2 { get; set; }


        public PolyLineNo PolyLineNo { get; set; }


        public bool HaveSentence { get; set; } = false;

        public string Sentence { get; set; }

        public int SentenceWidth { get; set; }

        public int SentenceHeight { get; set; }
        
        public int SentenceRect_X1 { get; set; }

        public int SentenceRect_Y1 { get; set; }

        public int SentenceRect_X2 { get; set; }

        public int SentenceRect_Y2 { get; set; }

        public int SentenceText_X1 { get; set; }

        public int SentenceText_Y1 { get; set; }


        // Y座標の調整済みフラグ。
        // false : 未調整
        // true  : 調整済み
        public bool Adjusted_Y { get; set; } = false;
    }



//public class MindRow_PolyLine
//{
//    public int No_Left { get; set; }

//    public int SuperParentNo_Left { get; set; }

//    public int No_Right { get; set; }

//    public int SuperParentNo_Right { get; set; }

//    public string Points { get; set; }
//}

    public class MindRow_Link
    {
        public short No { get; set; }

        public string Sentence { get; set; }

        public int X1 { get; set; }

        public int Y1 { get; set; }

        public int Width { get; set; }

    }



    public class MindRow
    {
        // プロパティ名は「No」禁止。「Id」じゃないと実行時エラーになる。

        public MindType Type { get; set; }

        public short OrderNo { get; set; }

        public int X1 { get; set; }

        public int Y1 { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int X2 { get; set; }

        public int Y2 { get; set; }
       
        public string Sentence { get; set; }

        public string URL { get; set; }

    }

    public class InversionColumnNo
    {
        public int ColumnNo;
        public int InversionNo;
    }

    public class SuperParent_ConnectionRelationship
    {
        public long SuperParentNo_Left;
        public long SuperParentNo_Right;
    }

    public class LineAdjusted
    {
        // 調整済みトップ親No
        public long? SuperParentNo;
    }

    public class NotConnectItemAdjusted
    {
        // 調整済みトップ親No
        public long? SuperParentNo;
    }

    public class PolyLine
    {
        // 始点
        public long No_Left;
        public long? SuperParentNo_Left;
        public int ItemCount_Left;
        public int X1;
        public int Y1;

        // 中間点
        public int X2;
        public int Y2;
        public int X3;
        public int Y3;

        // 終点
        public long No_Right;
        public long? SuperParentNo_Right;
        public int ItemCount_Right;
        public int X4;
        public int Y4;
    }


}
