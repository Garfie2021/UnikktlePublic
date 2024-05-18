using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Unikktle.Common;
using Unikktle.Data;
using Unikktle.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Localization;
using UnikktleCommon;

namespace Unikktle.Common
{
    public static class ClassConvertMind
    {
        private const short _Text_Start_X = 20;
        private const short _Text_Start_Y = 40;

        private const short _Text_1Item_Y_Height = 30;

        private const short _Margin_X_TextItems = 20;
        private const short _Margin_Y_BetweenTextItems = 60;
        private const short _Margin_Y_BetweenRectItems = 40;
        private const short _Margin_Y_Height = 20;  // アイテムとそのマージは同じ値

        private const short _Rect_Margin_X = 10;
        private const short _Rect_Margin_Y1 = 30;
        private const short _Rect_Margin_Y2 = 20;

        private const short _Line_Margin_X_Left = 10;
        private const short _Line_Margin_X_Right = -10;
        private const short _Line_Margin_X_SuperParentが同じ = 30;
        private const short _Line_Margin_Y = -5;
        private const short _Line_Sentence_Height = 30;

        private const int _SuperParent間の余白 = 50;
        private const short _SuperParent_Space = 100;


        public static void ClassConvert(
            ApplicationDbContext _dbContext,
            IStringLocalizer<SharedResource> _sharedLocalizer,
            string ItemRelationship,
            out List<MindRow_Text> textList,
            out List<MindRow_Link> linkList,
            out List<MindRow_Rect> rectList,
            out List<MindRow_Line> lineList,
            out string sentences,
            out string error)
        {
            textList = new List<MindRow_Text>();
            rectList = new List<MindRow_Rect>();
            linkList = new List<MindRow_Link>();
            lineList = new List<MindRow_Line>();
            sentences = "";

            var mindRelationRows = new List<MindRelationRow>();
            var mindItemList = new List<MindItemRow>();


            error = 入力チェック(_sharedLocalizer, ItemRelationship);

            if (!string.IsNullOrEmpty(error))
            {
                // エラー
                return;
            }

            ClassConvert_Item(_dbContext, ItemRelationship,
                ref mindItemList, ref mindRelationRows, //out string itemRelationship_ToNo, 
                out error);

            if (!string.IsNullOrEmpty(error))
            {
                // エラー
                return;
            }

            if (mindItemList.Count() < 1)
            {
                // エラー
                error = _sharedLocalizer["109"] + "\n" + error;
                return;
            }

            // 全文検索用の文字列を作成
            foreach (var mindItem in mindItemList)
            {
                sentences += mindItem.Sentence + " ";
            }

            // 各Itemに対応する、Textオブジェクトを作成。
            Text_ItemInitialize(mindItemList, ref textList);

            // 最初に依存（親子関係）を終わらせる。

            // この時点で、ParentNoは紐付け済み。
            Text_SetParentNo(mindRelationRows, ref textList);

            // 全Itemに最も親に当たるItemのNoを設定する。
            // この時点で、SuperParentNoは紐付け済み。
            Text_SetSuperParentNo_AllItem(ref mindRelationRows, ref textList, out List<SuperParent> superParentList);

            Text_LineType設定(ref mindRelationRows);

            Text_保持している子アイテムを階層と数をセット(ref textList);

            Text_依存_X1調整(ref textList);

            Text_依存_Y1調整(mindRelationRows, ref textList);

            //Text_依存_Y1調整_子アイテムのみ(ref textList);

            Text_X2調整(ref textList);

            // Y2で親子関係の高さを終わらせてから、
            Text_Y2調整(ref textList);

            // 接続関係があるアイテムのx座標を調整することで、接続関係があるアイテムのx座標の重複を解消する。
            // 左アイテムが同じ接続先のアイテムは、Y軸が重複するが、次のY軸調整で重複を解消する。
            // 右アイテムを、左アイテムの右へ移動。
            Text_接続関係_調整(mindItemList, mindRelationRows, ref textList);

            // 接続関係でx1を調整した後の、SuperParentのx1の値の違いを基準に、列Noを設定する
            Text_接続関係_SuperParent列No(mindItemList, ref mindRelationRows, ref textList, ref superParentList);

            // 接続関係で、rightアイテムとして１度も登場しなかったleftアイテムは、x1が左端になったままになる。
            // x1が左端になったままのアイテムは、接続関係があるrightアイテムとの距離が離れ過ぎるので、
            // 接続関係があるrightアイテムと接続関係があるleftアイテムの内、rightアイテムに一番近いleftアイテムとx1を合わせる。
            Text_接続関係_調整_離れ過ぎ(mindItemList, mindRelationRows, superParentList, ref textList);


            // 接続関係が１つでもある場合、依存関係はあっても接続関係のないアイテムは、右端に並べる。
            Text_依存関係有り_接続関係無し_調整(mindRelationRows, ref textList);

            // 依存関係、接続関係、どちらも無いItemは、依存関係はあっても接続関係のないアイテムの、更に右端に並べる。
            Text_依存関係無し_接続関係無し_調整(mindRelationRows, ref textList);

            // Y1 X1が重複するText、Rect、Lineは、Y軸の重複を解消する。
            Text_依存関係_接続関係_調整後の補正(ref textList);


            // y座標の重複を解消。
            // 親アイテム間で、Y軸の重複があれば重複を解消する。
            Text_PositionCalculation_DuplicationY_SuperParent以外(ref textList);

            // x1が同じSuperParentで、Y軸の重複があれば調整する。
            DuplicationY_Approach(ref textList, ref lineList);


            // Lineオブジェクト作成
            Line_ItemInitialize(mindItemList, mindRelationRows, textList, ref lineList);

            // Lineの色を調節
            Line_ColorAdjustment(ref lineList);

            // Lineの関係性を元にTextアイテムの配置を調整する
            Line_保有ITem数が少ない方のY座標を接続先に近付ける(lineList, ref textList);
            SuperParentのX1X2Y1Y2を最新の状態に更新(textList, ref superParentList);
            Line_保有ITem数が少ない方のY座標を接続先に近付ける_Y軸の重複を解消(superParentList, lineList, ref textList);

            // y座標の重複を解消。
            // Text_PositionCalculation_DuplicationY_SuperParent以外(ref textList); を流用してメソッドを追加し、
            // SuperParentのみを対象に重複をチェックし、重複していたら、そのSuperParentと子アイテムを移動する。
            Text_PositionCalculation_DuplicationY_SuperParent(ref textList);

            // Lineオブジェクトの位置を近付ける。
            Line_PositionApproach_Y座標(textList, ref lineList);

            // PolyLineオブジェクトのX位置を調整する。
            PolyLine_X_PositionApproach(ref lineList);


            // Lineに設定されているテキストが表示できるスペースを確保する。
            Line_SentenceSpace_Approach(ref textList, ref lineList);

            // Lineに設定されているテキスト位置を調整する。
            Line_Sentence_Approach(ref textList, ref lineList);

            // PolyLineに設定されているテキスト位置で重複があれば、Y軸を少し離す。
            PolyLine_Sentence_DuplicateApproach(ref lineList);

            // 傾いてるLineに設定されているテキストのY軸を調整する。
            Line_Sentence_TiltApproach(textList, ref lineList);


            // Rectオブジェクト作成
            // 計算済みtestのxy座標をベースに、Rectオブジェクト作成
            Rect_ItemInitialize(textList, ref rectList);
            ////Rect_PositionCalculation(ref rectList);
        }

        private static string 入力チェック(IStringLocalizer<SharedResource> _sharedLocalizer, string ItemRelationship)
        {
            // 行毎に処理
            foreach (var row in ItemRelationship.Split('\n'))
            {
                try
                {
                    if (string.IsNullOrEmpty(row))
                    {
                        // 改行コード。処理としては何もしない。入力可能なだけ。
                        continue;
                    }

                    var colums = row.Split('|');

                    if (colums.Count() < アイテム間の関係.列3_関係説明)
                    {
                        // 列が足りていません。 [{0}]
                        return _sharedLocalizer["100", row];
                    }

                    if (string.IsNullOrEmpty(colums[アイテム間の関係.列0_左アイテム名]))
                    {
                        // 1列目に値がありません。 [{0}]
                        return _sharedLocalizer["101", row];
                    }

                    if (string.IsNullOrEmpty(colums[アイテム間の関係.列2_右アイテム名]))
                    {
                        if (!string.IsNullOrEmpty(colums[アイテム間の関係.列1_関係]))
                        {
                            // 「右アイテム名」が無くて「関係」が有るのは間違ってる。

                            // 3列目に値が有りますが、2列目に関係記号がありません。 [{0}]
                            return _sharedLocalizer["116", row];
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(colums[アイテム間の関係.列1_関係]))
                        {
                            // 「右アイテム名」があって「関係」が無いのは間違ってる。

                            // 2列目に値がありません。 [{0}]
                            return _sharedLocalizer["102", row];
                        }

                        if (!string.IsNullOrEmpty(colums[アイテム間の関係.列1_関係]) &&
                            (colums[アイテム間の関係.列1_関係] == ">" || colums[アイテム間の関係.列1_関係] == "-") == false)
                        {
                            // 2列目が関係記号ではありません。 [{0}]
                            return _sharedLocalizer["107", row];
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionSt.ExceptionCommon(ex);
                    // 予期せぬエラーが発生しました。
                    return _sharedLocalizer["104", row];
                }
            }

            return "";
        }

        private static void ClassConvert_Item(ApplicationDbContext dbContext, string ItemRelationship,
            ref List<MindItemRow> mindItemList, ref List<MindRelationRow> mindRelationRows, out string error)
        {
            //itemRelationship_ToNo = null;
            error = null;
            var itemRelationshipNo = 0;

            // 行毎に処理
            foreach (var row in ItemRelationship.Split('\n'))
            {
                if (string.IsNullOrEmpty(row))
                {
                    // 改行コード。処理としては何もしない。入力可能なだけ。
                    continue;
                }

                var colums = row.Split('|');

                // 左側をアイテム一覧に保持
                var keywordNo_Left = SP_MindKeyword.GetInsert(dbContext, colums[アイテム間の関係.列0_左アイテム名]);

                if (!mindItemList.Any(x => x.Sentence == colums[アイテム間の関係.列0_左アイテム名]))
                {
                    mindItemList.Add(new MindItemRow()
                    {
                        ItemType = ItemType.Item,
                        KeywordNo = keywordNo_Left,
                        ItemRelationshipNo = itemRelationshipNo,
                        Sentence = colums[アイテム間の関係.列0_左アイテム名],
                        SentenceWidth = SP_TextWidth.GetTextWidth(dbContext, colums[アイテム間の関係.列0_左アイテム名])
                    });
                }

                //if (string.IsNullOrEmpty(colums[アイテム間の関係.列2_右アイテム名]))
                //{
                //    // 右側アイテムが無ければ、以降の関係処理はスキップ。
                //    continue;
                //}

                long keywordNo_Right = 0;
                if (!string.IsNullOrEmpty(colums[アイテム間の関係.列2_右アイテム名]))
                {
                    // 右側をアイテム一覧に保持
                    keywordNo_Right = SP_MindKeyword.GetInsert(dbContext, colums[アイテム間の関係.列2_右アイテム名]);

                    if (!mindItemList.Any(x => x.Sentence == colums[アイテム間の関係.列2_右アイテム名]))
                    {
                        mindItemList.Add(new MindItemRow()
                        {
                            ItemType = ItemType.Item,
                            KeywordNo = keywordNo_Right,
                            ItemRelationshipNo = itemRelationshipNo,
                            Sentence = colums[アイテム間の関係.列2_右アイテム名],
                            SentenceWidth = SP_TextWidth.GetTextWidth(dbContext, colums[アイテム間の関係.列2_右アイテム名])
                        });
                    }
                }

                // アイテム間の関係説明のKeywordNo
                long KeywordNo_Sentence = 0;
                if (!string.IsNullOrEmpty(colums[アイテム間の関係.列3_関係説明]))
                {
                    // アイテム間の関係説明をアイテム一覧に保持
                    KeywordNo_Sentence = SP_MindKeyword.GetInsert(dbContext, colums[アイテム間の関係.列3_関係説明]);

                    if (!mindItemList.Any(x => x.Sentence == colums[アイテム間の関係.列3_関係説明]))
                    {
                        mindItemList.Add(new MindItemRow()
                        {
                            ItemType = ItemType.Sentence,
                            KeywordNo = KeywordNo_Sentence,
                            ItemRelationshipNo = itemRelationshipNo,
                            Sentence = colums[アイテム間の関係.列3_関係説明],
                            SentenceWidth = SP_TextWidth.GetTextWidth(dbContext, colums[アイテム間の関係.列3_関係説明])
                        });
                    }
                }

                // 左側、右側をリレーション一覧に保持
                var mindRelationRow = new MindRelationRow()
                {
                    ItemRelationshipNo = itemRelationshipNo,
                    Left_KeywordNo = keywordNo_Left,
                    Left_KeywordSentence = colums[アイテム間の関係.列0_左アイテム名],
                    Right_KeywordNo = keywordNo_Right,
                    Right_KeywordSentence = colums[アイテム間の関係.列2_右アイテム名],
                    KeywordNo_Sentence = KeywordNo_Sentence,
                    Sentence = colums[アイテム間の関係.列3_関係説明]
                };

                if (colums[アイテム間の関係.列1_関係] == ">")
                {
                    mindRelationRow.RelationType = RelationType.依存;
                }
                else if (colums[アイテム間の関係.列1_関係] == "-")
                {
                    mindRelationRow.RelationType = RelationType.接続;
                }
                //else
                //{
                //    // なんらかの関係はあるが、依存も接続もしていない
                //    mindRelationRow.RelationType = RelationType.無関係;
                //}

                mindRelationRows.Add(mindRelationRow);

                itemRelationshipNo++;

                // アイテム名をアイテムNoに変換した文字列を保持
                //itemRelationship_ToNo += mindRelationRow.KeywordNo_Left + "|" + colums[1] + "|" + mindRelationRow.KeywordNo_Right + ;
            }
        }

        private static void Text_ItemInitialize(List<MindItemRow> mindItemRow,
            ref List<MindRow_Text> textList)
        {
            foreach (var item in mindItemRow.Where(x => x.ItemType == ItemType.Item))
            {
                var text = new MindRow_Text()
                {
                    KeywordNo = item.KeywordNo,
                    ItemRelationshipNo = item.ItemRelationshipNo,
                    Sentence = item.Sentence,
                    ChildColumnNo = Consts.StartColumnNoNumber,
                    X1 = _Text_Start_X,
                    Y1 = _Text_Start_Y
                };

                text.X2 = text.X1 + mindItemRow.First(x => x.KeywordNo == text.KeywordNo).SentenceWidth;
                text.Y2 = text.Y1 + _Text_1Item_Y_Height;

                textList.Add(text);
            }
        }

        private static void Text_SetParentNo(List<MindRelationRow> mindRelationRows,
            ref List<MindRow_Text> textList)
        {
            foreach (var item in mindRelationRows.Where(x => x.RelationType == RelationType.依存))
            {
                var leftItem = textList.First(x => x.KeywordNo == item.Left_KeywordNo);
                var rightItem = textList.First(x => x.KeywordNo == item.Right_KeywordNo);

                // 親ItemのNoを設定。
                rightItem.ParentNo = leftItem.KeywordNo;

                // 子Itemは親Itemの列Noより、１つ右側にする。
                rightItem.ChildColumnNo = leftItem.ChildColumnNo + 1;

                // 子Itemに親Itemの何番目の子アイテムか設定。
                rightItem.ChildRowNo = leftItem.ChildCount;

                // 親アイテムの子アイテムカウンタをインクリメント
                leftItem.ChildCount++;
            }
        }

        // 全Itemに、最も親に当たるItemのNoを設定する。
        private static void Text_SetSuperParentNo_AllItem(ref List<MindRelationRow> mindRelationRows, ref List<MindRow_Text> textList, 
            out List<SuperParent> superParentList)
        {
            foreach (var item in textList)
            {
                // Text Item
                item.SuperParentNo = GetSuperParentNo(textList, item.KeywordNo);
                item.SuperParent_Sentence = textList.First(x => x.KeywordNo == item.SuperParentNo).Sentence;

                // 後の処理の為に、MindRelationRowにSuperParentNoを持たせておく
                foreach (var mindRelation in mindRelationRows.Where(x => x.Left_KeywordNo == item.KeywordNo))
                {
                    mindRelation.Left_SuperParentNo = item.SuperParentNo;
                    mindRelation.Left_SuperParent_Sentence = item.SuperParent_Sentence;
                }

                foreach (var mindRelation in mindRelationRows.Where(x => x.Right_KeywordNo == item.KeywordNo))
                {
                    mindRelation.Right_SuperParentNo = item.SuperParentNo;
                    mindRelation.Right_SuperParent_Sentence = item.SuperParent_Sentence;
                }

                // Rect Item
                //rectList.First(x => x.No == item.No).SuperParentNo = item.SuperParentNo;
            }

            // SuperParentのリストを作成。後の計算を楽にするため。
            superParentList = new List<SuperParent>();
            foreach (var item in textList.Where(x => x.SuperParentNo == x.KeywordNo))
            {
                superParentList.Add(new SuperParent() 
                { 
                    KeywordNo = item.KeywordNo,
                    Sentence = item.Sentence
                });
            }

            //// SuperParentのItemかどうかを明確にする。
            //foreach (var item in textList.Where(x => x.No == x.SuperParentNo))
            //{
            //    item.SuperParent = true;
            //}
        }

        private static void Text_LineType設定(ref List<MindRelationRow> mindRelationRows)
        {
            foreach (var mindRelationRow in mindRelationRows.Where(x => x.RelationType == RelationType.接続))
            {
                if (mindRelationRow.Left_SuperParentNo == mindRelationRow.Right_SuperParentNo)
                {
                    mindRelationRow.LineType = LineType.SuperParentが同じ;
                }
                else if (mindRelationRow.Left_SuperParentNo != mindRelationRow.Right_SuperParentNo)
                {
                    mindRelationRow.LineType = LineType.SuperParentが違う;
                }
            }
        }

        private static void Text_保持している子アイテムを階層と数をセット(ref List<MindRow_Text> textList)
        {
            foreach (var item in textList)
            {
                int 保持している全子アイテムの数 = 0;
                保持している子アイテム数を取得(item, textList, ref 保持している全子アイテムの数);
                item.保持している全子アイテムの数 = 保持している全子アイテムの数;

                //保持している子アイテムの階層数を取得(item, textList, ref 保持している全子アイテムの階層数);
                var maxChildColumnNo = item.ChildColumnNo;
                一番大きいChildColumnNo取得(item, textList, ref maxChildColumnNo);
                item.保持している子アイテムの階層 = maxChildColumnNo - item.ChildColumnNo;
            }
        }

        private static void Text_依存_X1調整(ref List<MindRow_Text> textList)
        {
            foreach (var text in textList)
            {
                var plusX = text.ChildColumnNo * _Margin_X_TextItems;

                text.X1 += plusX;
                text.X2 += plusX;
            }
        }

        private static void Text_依存_Y1調整(List<MindRelationRow> mindRelationRows, ref List<MindRow_Text> textList)
        {
            foreach (var item in mindRelationRows.Where(x => x.RelationType == RelationType.依存))
            {
                var leftItem = textList.First(x => x.KeywordNo == item.Left_KeywordNo);
                var rightItem = textList.First(x => x.KeywordNo == item.Right_KeywordNo);

                // rightItemと同じ親を持つ、自分自身以外の子アイテムを抽出
                //var childItems = textList.Where(x => x.ParentNo == leftItem.No && x.No != rightItem.No);
                //var y2 = textList.Where(x => x.ParentNo == leftItem.No && x.No != rightItem.No).Max(x => x.Y2);

                if (leftItem.ChildItemCount < 1)
                {
                    // 同じ親を持つ、子Itemの内、最初の子Itemは、親Itemの下に配置する。
                    rightItem.Y1 = leftItem.Y1 + _Margin_Y_BetweenTextItems;
                }
                else
                {
                    // 抽出した子アイテムの一番下
                    //var y2 = childItems.Max(x => x.Y2);
                    //var childItem2 = Y1が一番下になるItemを探索して返す_再帰(textList, childItem);

                    // 同じ親を持つ、子Itemの内、最も下にある子Itemの、更に下に配置する。
                    rightItem.Y1 = textList.Where(x => x.ParentNo == leftItem.KeywordNo).Max(x => x.Y2) + _Margin_Y_Height + 10;
                }

                // 右アイテムの高さを調整
                rightItem.Y2 = rightItem.Y1 + _Text_1Item_Y_Height;

                leftItem.ChildItemCount++;
            }
        }

        private static void Text_X2調整(ref List<MindRow_Text> textList)
        {
            // 子、孫、ひ孫と、配下にあるアイテム全てで、最もX2が大きいアイテムのX2を返す。再帰。
            foreach (var item in textList)
            {
                var x2 = Max_ChildX2(item, textList);

                if (x2 == null)
                {
                    continue;
                }

                item.X2 = (int)x2;
            }

            // 保持している子アイテムの階層分、マージンを加える
            foreach (var item in textList)
            {
                item.X2 += (item.保持している子アイテムの階層) * _Margin_X_TextItems;
            }
        }

        private static void Text_Y2調整(ref List<MindRow_Text> textList)
        {
            foreach (var text in textList)
            {
                text.Y2 += (text.保持している全子アイテムの数 * _Text_1Item_Y_Height) +
                    text.保持している子アイテムの階層 * (_Text_1Item_Y_Height + _Margin_Y_Height);
            }
        }

        private static void Text_接続関係_SuperParent列No(List<MindItemRow> mindItemList, ref List<MindRelationRow> mindRelationRows,
            ref List<MindRow_Text> textList, ref List<SuperParent> superParentList)
        {
            // 計算し易くなるよう、x1をセットしておく
            foreach (var superParent in superParentList)
            {
                superParent.X1 = textList.First(x => x.KeywordNo == superParent.KeywordNo).X1;
            }

            // X1で昇順にソート
            superParentList.Sort((a, b) => a.X1 - b.X1);

            var x1 = 0;
            var 列No = 0;
            foreach (var superParent in superParentList)
            {
                if (x1 < superParent.X1)
                {
                    // superParentのx1が増える毎に、列Noをインクリメントし、それを列Noとして使う
                    x1 = superParent.X1;
                    列No++;
                }

                // 列Noをセット
                superParent.SuperParent列No = 列No;
            }

            // MindRelationRowsにも反映
            foreach (var mindRelation in mindRelationRows)
            {
                if (mindRelation.Left_SuperParentNo != null)
                {
                    mindRelation.Left_SuperParent列No = superParentList.First(x => x.KeywordNo == mindRelation.Left_SuperParentNo).SuperParent列No;
                }

                if (mindRelation.Right_SuperParentNo != null)
                {
                    mindRelation.Right_SuperParent列No = superParentList.First(x => x.KeywordNo == mindRelation.Right_SuperParentNo).SuperParent列No;
                }
            }

            //var mindRelationRows接続のみ = mindRelationRows.Where(x => x.RelationType == RelationType.接続);

            //// SuperParent列Noは、RightアイテムにRelationしてるLeftアイテムの数と同義
            //foreach (var superParent in superParentList)
            //{
            //    superParent.SuperParent列No = mindRelationRows接続のみ.Count(x => x.Right_SuperParentNo == superParent.KeywordNo);
            //}

            //foreach (var mindRelationRow in mindRelationRows.Where(x => x.RelationType == RelationType.接続))
            //{
            //    var leftSuperParent = superParentList.First(x => x.KeywordNo == mindRelationRow.Left_SuperParentNo);
            //    var rightSuperParent = superParentList.First(x => x.KeywordNo == mindRelationRow.Right_SuperParentNo);

            //    rightSuperParent.SuperParent列No = leftSuperParent.SuperParent列No + 1;
            //}
        }

        // x座標の重複を解消。
        // 接続関係があるアイテムのx座標を調整することで、接続関係があるアイテムのx座標の重複を解消する。
        // 左アイテムが同じ接続先のアイテムは、Y軸が重複するが、次のY軸調整で重複を解消する。
        private static void Text_接続関係_調整(List<MindItemRow> mindItemList, List<MindRelationRow> mindRelationRows,
            ref List<MindRow_Text> textList)
        {
            //var superParent_ConnectionRelationship = new List<SuperParent_ConnectionRelationship>();

            //// SuperParentNoのリスト作成
            //var superParentNoList = new List<long>();
            //foreach (var item in textList)
            //{
            //    if (superParentNoList.Any(x => x == item.SuperParentNo))
            //    {
            //        continue;
            //    }

            //    superParentNoList.Add(item.SuperParentNo);
            //}

            foreach (var item in mindRelationRows.Where(x => x.RelationType == RelationType.接続))
            {
                var leftItem = textList.First(x => x.KeywordNo == item.Left_KeywordNo);
                var rightItem = textList.First(x => x.KeywordNo == item.Right_KeywordNo);

                if (leftItem.SuperParentNo == rightItem.SuperParentNo)
                {
                    // Topの親が同じなので、配置調整対象外。
                    continue;
                }

                var leftItem_SuperParent = textList.First(x => x.KeywordNo == leftItem.SuperParentNo);
                var rightItem_SuperParent = textList.First(x => x.KeywordNo == rightItem.SuperParentNo);

                if (leftItem_SuperParent.X1 < rightItem_SuperParent.X1)
                {
                    // SuperParentのX1が重複してないので、調整済み。
                    continue;
                }

                var 初期位置との差分 = _Text_Start_X - rightItem_SuperParent.X1;
                
                //var 移動x = leftItem_SuperParent.X2 + mindItemList.First(x => x.KeywordNo == item.KeywordNo_Sentence).SentenceWidth;
                var 移動x = leftItem_SuperParent.X2 + _SuperParent_Space;

                // 右アイテムのSuperParent配下にある子アイテムは全て、左SuperParentの範囲外の右へ移動する。
                foreach (var rightItem2 in textList.Where(x => x.SuperParentNo == rightItem_SuperParent.KeywordNo))
                {
                    // 一旦、初期位置に戻してから移動する
                    rightItem2.X1 += 初期位置との差分 + 移動x;
                    rightItem2.X2 += 初期位置との差分 + 移動x;
                }

                //var leftItem_SuperParent_X2 = textList.First(x => x.No == leftItem.SuperParentNo).X2;
                //var rightItem_SuperParent_X1 = textList.First(x => x.No == rightItem.SuperParentNo).X1;

                //if ((rightItem_SuperParent_X1 - leftItem_SuperParent_X2) < Space)
                //{
                //    if (mindRelationRow.Count(x => x.RelationType == RelationType.接続 && x.KeywordNo_Left == item.KeywordNo_Left) > 1)
                //    {
                //        // 左Itemが複数のItemに接続している場合、左側以外のItemは全て、右側に移動する。
                //        foreach (var item2 in textList.Where(x => x.SuperParentNo != leftItem.SuperParentNo))
                //        {
                //            item2.X1 += leftItem_SuperParent_X2 + Space;  // 左側と右側の余白は100px。
                //            item2.X2 = item2.X1 + item2.Width;
                //        }
                //    }
                //    else
                //    {
                //        // 右側のItemに親子関係のあるItemの内、左右の配置が未調整なら調整する。
                //        foreach (var item2 in textList.Where(x => x.SuperParentNo == rightItem.SuperParentNo))
                //        {
                //            item2.X1 += leftItem_SuperParent_X2 + Space;  // 左側と右側の余白は100px。
                //            item2.X2 = item2.X1 + item2.Width;
                //        }
                //    }
                //}
            }
        }

        private static void Text_接続関係_調整_離れ過ぎ(List<MindItemRow> mindItemList, List<MindRelationRow> mindRelationRows,
            List<SuperParent> superParentList, ref List<MindRow_Text> textList)
        {
            // superParentのみのループ
            foreach (var superParent in superParentList)
            //foreach (var textSuperParent in textList.Where(x => x.KeywordNo == x.SuperParentNo).Where(x => x.X1 == _Text_Start_X))
            {
                // superParentが接続している右側のSuperParentリストを作る
                var mindRelationRows_接続してる右 = 
                    mindRelationRows.Where(x => x.RelationType == RelationType.接続 && x.Left_SuperParentNo == superParent.KeywordNo);

                if (mindRelationRows_接続してる右.Count() < 1)
                {
                    // 接続関係が何もなければスキップする
                    continue;
                }

                if (superParent.SuperParent列No >= mindRelationRows_接続してる右.Min(x => x.Right_SuperParent列No) - 1)
                {
                    // 一番近いsuperParentの列Noと、差が1列分なら、調整する必要はない
                    continue;
                }

                // 接続先のsuperParentListを作る（superParentListClone）
                var mindRelationRow同じ右SuperParentに接続している = new List<MindRelationRow>();
                foreach (var 接続してる右 in mindRelationRows_接続してる右)
                {
                    if (接続してる右.LineType == LineType.SuperParentが同じ)
                    {
                        //「LineType.SuperParentが同じ」は関係無いので除外
                        continue;
                    }

                    mindRelationRow同じ右SuperParentに接続している.AddRange(
                        mindRelationRows.Where(x => 
                            x.RelationType == RelationType.接続 && x.LineType == LineType.SuperParentが違う && 
                            x.Right_SuperParentNo == 接続してる右.Right_SuperParentNo));
                }

                //var a = mindRelationRows_接続してる右.Where(x => x.Right_SuperParentNo == superParentListClone.);

                var superParentList同じ右SuperParentに接続している = new List<SuperParent>();
                foreach (var 接続してる右 in mindRelationRow同じ右SuperParentに接続している)
                {
                    superParentList同じ右SuperParentに接続している.AddRange(
                        superParentList.Where(x => x.KeywordNo == 接続してる右.Left_SuperParentNo));
                }

                var 同じ右アイテムに接続してる左アイテムの中で一番右にあるsuperParent = 
                    superParentList同じ右SuperParentに接続している.OrderByDescending(x => x.SuperParent列No).First();


                //var 列NoがあいだになっているsuperParentは接続関係がある = false;
                //var 接続数 = 0;
                //foreach (var 列NoがあいだになっているsuperParent in 
                //    superParentList.Where(x => x.SuperParent列No == 同じ右アイテムに接続してる左アイテムの中で一番右にあるsuperParent.SuperParent列No - 1))
                //{
                //    var mindRelationRows_列NoがあいだになっているsuperParentに接続 = mindRelationRows.Where(x => x.Left_SuperParentNo == 列NoがあいだになっているsuperParent.KeywordNo ||
                //        x.Right_SuperParentNo == 列NoがあいだになっているsuperParent.KeywordNo);

                //    var result = mindRelationRows_列NoがあいだになっているsuperParentに接続
                //        .Where(x => x.Left_SuperParentNo == superParent.KeywordNo || x.Right_SuperParentNo == superParent.KeywordNo);

                //    接続数 += result.Count();

                //    if (接続数 > 0)
                //    {
                //        break;
                //    }
                //}

                //if (接続数 < 1)
                //{
                //    // 列NoがあいだになっているsuperParentが、接続関係の無いsuperParentだったら調整する必要はない
                //    continue;
                //}

                //var x1を合わせる先のsuperParent = 
                //    superParentList.First(x => x.SuperParent列No == 同じ右アイテムに接続してる左アイテムの中で一番右にあるsuperParent.SuperParent列No - 1);

                var 移動x = 同じ右アイテムに接続してる左アイテムの中で一番右にあるsuperParent.X1 - superParent.X1;

                // 左アイテムのSuperParent配下にある子アイテムは全て移動する。
                foreach (var text in textList.Where(x => x.SuperParentNo == superParent.KeywordNo))
                {
                    // 一旦、初期位置に戻してから移動する
                    text.X1 += 移動x;
                    text.X2 += 移動x;
                }

                // x1を合わせたsuperParentの列Noも合わせる
                superParent.SuperParent列No = 同じ右アイテムに接続してる左アイテムの中で一番右にあるsuperParent.SuperParent列No;

                //if (!mindRelationRows.Any(x => x.RelationType == RelationType.接続 && x.Left_SuperParentNo == superParent.KeywordNo))
                //{
                //    // 調整済みは除外
                //    continue;
                //}

                //var x1調整先 = 0;

                //// superParentの接続関係ループ
                //foreach (var leftMindRelation in mindRelationRows.Where(x => x.Left_SuperParentNo == superParent.KeywordNo))
                //{
                //    // leftアイテムに接続関係があるrightアイテム
                //    foreach (var rightMindRelation in mindRelationRows.Where(x => x.Right_KeywordNo == leftMindRelation.Right_KeywordNo))
                //    {
                //        // rightアイテムに接続しているleftアイテムの中で、一番右にあるsuperParentのleftアイテムのx1を抽出

                //        var x1 = textList.First(x => x.KeywordNo == rightMindRelation.Left_KeywordNo).X1;

                //        if (x1調整先 < x1)
                //        {
                //            x1調整先 = x1;
                //        }
                //    }
                //}

                //if (superParent.X1 == x1調整先)
                //{
                //    // 抽出したx1と一致している場合、x1の移動は不要。
                //    continue;
                //}

                //var 移動x = x1調整先 - superParent.X1;

                //// 左アイテムのSuperParent配下にある子アイテムは全て移動する。
                //foreach (var rightItem2 in textList.Where(x => x.SuperParentNo == superParent.KeywordNo))
                //{
                //    // 一旦、初期位置に戻してから移動する
                //    rightItem2.X1 += 移動x;
                //    rightItem2.X2 += 移動x;
                //}
            }
        }

        private static void Text_依存関係_接続関係_調整後の補正(ref List<MindRow_Text> textList)
        {
            while (true)
            {
                var 重複Count1 = Text_依存関係_接続関係_調整後の補正_X1Y1が重複するアイテムはY軸の重複を解消する(ref textList);

                var 重複Count2 = Text_依存関係_接続関係_調整後の補正_Y2調整_親子(ref textList);

                if (重複Count1 < 1 && 重複Count2 < 1)
                {
                    // ２つとも、どちらかの影響を受けても、重複が発生しなくなった。
                    break;
                }
            }

            // 上のループでカバーしきれないパターンに対処
            Text_依存関係_接続関係_調整後の補正_X1Y1が重複するアイテムはY軸の重複を解消する_更に(ref textList);
        }

        private static int Text_依存関係_接続関係_調整後の補正_X1Y1が重複するアイテムはY軸の重複を解消する(ref List<MindRow_Text> textList)
        {
            var 重複Count = 0;
            var Duplication1 = false;
            var Duplication2 = false;

            while (true)
            {
                foreach (var text in textList)
                {
                    // Textのエリアだけではなく、Rectの枠も加味して、重複するアイテムを抽出する
                    var 重複texts = textList.Where(x =>
                        x.X1 == text.X1 &&
                        //(text.X1 - _Rect_Margin_X) <= x.X1 &&
                        //x.X1 <= (text.X2 + _Rect_Margin_X) &&
                        (text.Y1 - _Rect_Margin_Y1) <= x.Y1 &&
                        x.Y1 <= (text.Y2 + _Rect_Margin_Y2) &&
                        x.KeywordNo != text.KeywordNo);

                    if (重複texts.Count() < 1)
                    {
                        // 重複無し。
                        continue;
                    }

                    foreach (var duplicationText in 重複texts)
                    {
                        var y差 = text.Y2 - duplicationText.Y1 + _Margin_Y_BetweenRectItems;

                        duplicationText.Y1 += y差;
                        duplicationText.Y2 += y差;

                        // 重複してるアイテムに子アイテムがいたら、それらも移動する
                        X1Y1が重複するアイテムはY軸の重複を解消する_子アイテム再帰(y差, duplicationText, ref textList);
                    }

                    // 子アイテムのY座標を変更したことで、親アイテムのY2の調整が必要

                    // 移動したアイテムと同じ親を持つ子アイテムの内、最も下のY2を取得
                    var 最も下のY2 = textList.Where(x => x.ParentNo == text.ParentNo).Max(x => x.Y2);

                    // SuperParent以外は、親のY2を広げる
                    if (text.ParentNo != null)
                    {
                        var parent = textList.First(x => x.KeywordNo == text.ParentNo);

                        if (parent.Y2 < 最も下のY2)
                        {
                            // 親アイテムのY2をはみ出しているので、親アイテムのY2を広げる。
                            parent.Y2 += 最も下のY2 - parent.Y2 + _Margin_Y_Height;
                        }
                    }

                    Duplication1 = true;
                    重複Count++;
                }

                // 2回連続で重複を、重複が有るかの判定にする
                Duplication2 = Duplication1;
                Duplication1 = false;
                if (!Duplication1 && !Duplication2)
                {
                    break;
                }
            }

            return 重複Count;
        }

        private static int Text_依存関係_接続関係_調整後の補正_X1Y1が重複するアイテムはY軸の重複を解消する_更に(ref List<MindRow_Text> textList)
        {
            var 重複Count = 0;
            var Duplication1 = false;
            var Duplication2 = false;

            while (true)
            {
                foreach (var text in textList)
                {
                    // Textのエリアだけではなく、Rectの枠も加味して、重複するアイテムを抽出する
                    var 重複texts = textList.Where(x =>
                        x.Y1 == text.Y1 &&
                        (text.X1 - _Rect_Margin_X) <= x.X1 &&
                        x.X1 <= (text.X2 + _Rect_Margin_X) &&
                        x.KeywordNo != text.KeywordNo &&
                        x.SuperParentNo != text.SuperParentNo);

                    if (重複texts.Count() < 1)
                    {
                        // 重複無し。
                        continue;
                    }

                    foreach (var duplicationText in 重複texts)
                    {
                        var y差 = text.Y2 - duplicationText.Y1 + _Margin_Y_BetweenRectItems;

                        duplicationText.Y1 += y差;
                        duplicationText.Y2 += y差;

                        // 重複してるアイテムに子アイテムがいたら、それらも移動する
                        X1Y1が重複するアイテムはY軸の重複を解消する_子アイテム再帰(y差, duplicationText, ref textList);
                    }

                    Duplication1 = true;
                    重複Count++;
                }

                // 2回連続で重複を、重複が有るかの判定にする
                Duplication2 = Duplication1;
                Duplication1 = false;
                if (!Duplication1 && !Duplication2)
                {
                    break;
                }
            }

            return 重複Count;
        }


        // 子アイテムのY2の最大値が、親アイテムのY2より大きかったら、補正する
        private static int Text_依存関係_接続関係_調整後の補正_Y2調整_親子(ref List<MindRow_Text> textList)
        {
            var 重複Count = 0;
            var Duplication1 = false;
            var Duplication2 = false;

            while (true)
            {
                foreach (var text in textList)
                {
                    if (text.ChildItemCount < 1)
                    {
                        // 問題無し
                        continue;
                    }

                    var childMaxY2 = textList.Where(x => x.ParentNo == text.KeywordNo).Max(x => x.Y2);

                    if (text.Y2 - _Margin_Y_Height >= childMaxY2)
                    {
                        // 問題無し
                        continue;
                    }

                    text.Y2 = childMaxY2 + _Margin_Y_Height;

                    Duplication1 = true;
                    重複Count++;
                }

                // 2回連続で重複を、重複が有るかの判定にする
                Duplication2 = Duplication1;
                Duplication1 = false;
                if (!Duplication1 && !Duplication2)
                {
                    break;
                }
            }

            return 重複Count;
        }

        // どのアイテムにも接続しない、依存関係の無いアイテムは、右端に並べる。
        private static void Text_依存関係有り_接続関係無し_調整(List<MindRelationRow> mindRelationRows,
            ref List<MindRow_Text> textList)
        {
            // 接続関係が有るアイテムの内、右アイテムのX2の最大値を取得し、全体の右端として使う
            var maxX2 = 0;
            foreach (var item in mindRelationRows.Where(x => x.RelationType == RelationType.接続))
            {
                var text = textList.First(x => x.KeywordNo == item.Right_KeywordNo);
                var x2 = textList.First(x => x.KeywordNo == text.SuperParentNo).X2;
                if (maxX2 < x2)
                {
                    maxX2 = x2;
                }
            }

            maxX2 += _SuperParent間の余白;

            var 調整済みSuperParent = new List<long>();
            var 対象外SuperParent = new List<long>();

            // 接続関係の無い、依存関係のみのアイテムを移動する。
            // maxX2に変化は無くても、Y軸で重複することがあるので、このループは常に必要。
            var count = 0;
            foreach (var item in mindRelationRows.Where(x => x.RelationType == RelationType.依存))
            {
                // 依存関係は左右どちらか片方だけで良い。SuperParentNoは同じ。
                var text = textList.First(x => x.KeywordNo == item.Left_KeywordNo);
                var superParentNo = (long)text.SuperParentNo;

                if (調整済みSuperParent.Any(x => x == superParentNo) || 対象外SuperParent.Any(x => x == superParentNo))
                {
                    // 既に処理した、SuperParentアイテムと依存関係のあるアイテムなので、スキップする。
                    continue;
                }

                // 接続関係のあるアイテムを含んでいるSuperParentは対象外
                if (接続関係のあるアイテムを含んでいるSuperParentは対象外(superParentNo, mindRelationRows, textList))
                {
                    // 既に処理した、SuperParentアイテムと依存関係のあるアイテムなので、スキップする。
                    対象外SuperParent.Add(superParentNo);
                    continue;
                }

                // 調整対象のSuperParent
                foreach (var text2 in textList.Where(x => x.SuperParentNo == superParentNo))
                {
                    text2.X1 += maxX2;
                    text2.X2 += maxX2;
                }

                調整済みSuperParent.Add(superParentNo);

                //count++;
            }

            // 調整対象のアイテムの、最小Y1を計算し、その値を、全体を上に移動する際の基準値とする。
            var minY1 = textList.Max(x => x.Y2);
            foreach (var superParentNo in 調整済みSuperParent)
            {
                var y1 = textList.First(x => x.KeywordNo == superParentNo).Y1;

                if (minY1 > y1)
                {
                    minY1 = y1;
                }
            }

            minY1 -= _Text_Start_Y;

            foreach (var superParentNo in 調整済みSuperParent)
            {
                foreach (var text2 in textList.Where(x => x.SuperParentNo == superParentNo))
                {
                    text2.Y1 -= minY1;
                    text2.Y2 -= minY1;

                    //text.Y1 = _Text_Start_Y + count * (_Text_1Item_Y_Height + _Margin_Y_Height + _Margin_Y_Height);
                    //text.Y2 = text.Y1 + _Text_1Item_Y_Height;
                }
            }

        }

        private static bool 接続関係のあるアイテムを含んでいるSuperParentは対象外(long superParentNo, 
            List<MindRelationRow> mindRelationRows, List<MindRow_Text> textList)
        {
            foreach (var text2 in textList.Where(x => x.SuperParentNo == superParentNo))
            {
                foreach (var item2 in mindRelationRows.Where(x => x.RelationType == RelationType.接続))
                {
                    if (text2.KeywordNo == item2.Left_KeywordNo || text2.KeywordNo == item2.Right_KeywordNo)
                    {
                        // 調整不要
                        return true;
                    }
                }
            }

            // 調整必要
            return false;
        }

        // どのアイテムにも接続しない、依存関係の無いアイテムは、右端に並べる。
        private static void Text_依存関係無し_接続関係無し_調整(List<MindRelationRow> mindRelationRows,
            ref List<MindRow_Text> textList)
        {
            //const int _高さ = 80;  // 無関係アイテムのエリアの高さ。無関係アイテムは常にソロ。１つ分で十分の高さがあれば良い。

            // 元データを変更しないようにコピーを作成。
            var textListCopy = textList.ToList();

            // 無関係アイテムを除外してから、X2の最大値を取得し、全体の右端として使う。
            foreach (var item in mindRelationRows.Where(x => x.RelationType == RelationType.無関係))
            {
                textListCopy.RemoveAll(x => x.KeywordNo == item.Left_KeywordNo);
            }

            var maxX2 = _SuperParent間の余白;
            if (textListCopy.Count() > 0)
            {
                maxX2 += textListCopy.Max(x => x.X2);
            }

            // maxX2に変化は無くても、Y軸で重複することがあるので、このループは常に必要
            var count = 0;
            foreach (var item in mindRelationRows.Where(x => x.RelationType == RelationType.無関係))
            {
                var text = textList.First(x => x.KeywordNo == item.Left_KeywordNo);

                text.X1 += maxX2;
                text.X2 += maxX2;

                text.Y1 = _Text_Start_Y + count * (_Text_1Item_Y_Height + _Margin_Y_Height + _Margin_Y_Height);
                text.Y2 = text.Y1 + _Text_1Item_Y_Height;

                count++;
            }

            //// 元データを変更しないようにコピーを作成。
            //var mindItemRowCopy = mindItemRow.ToList();

            //var firstItem = false;
            //var startX = 0;
            //var mindItemRelationRow_Connecction = mindRelationRow.Where(x => x.RelationType == RelationType.接続);
            //if (mindItemRelationRow_Connecction.Count() < 1)
            //{
            //    // 接続関係は無い。

            //    startX = 10;    // 余白
            //    firstItem = true;
            //}
            //else
            //{
            //    // コピーから接続関係のあるデータを除外して、接続関係の無いデータを作成。

            //    foreach (var row in mindItemRelationRow_Connecction)
            //    {
            //        var superParentNo = textList.First(x => x.No == row.KeywordNo_Left).SuperParentNo;
            //        foreach (var rect in textList.Where(x => x.SuperParentNo == superParentNo))
            //        {
            //            mindItemRowCopy.RemoveAll(x => x.No == rect.No);
            //        }

            //        superParentNo = textList.First(x => x.No == row.KeywordNo_Right).SuperParentNo;
            //        foreach (var rect in textList.Where(x => x.SuperParentNo == superParentNo))
            //        {
            //            mindItemRowCopy.RemoveAll(x => x.No == rect.No);
            //        }
            //    }
            //}

            //var listAdjusted = new List<NotConnectItemAdjusted>();

            //// 接続関係の無いデータの座標を調整。
            //var maxX2 = 0;
            //foreach (var row in mindItemRowCopy)
            //{
            //    if (firstItem)
            //    {
            //        maxX2 = startX;
            //        firstItem = false;
            //    }
            //    else
            //    {
            //        maxX2 = textList.Max(x => x.X2) + _Space;
            //    }

            //    var superParentNo = textList.First(x => x.No == row.No).SuperParentNo;

            //    if (listAdjusted.Any(x => x.SuperParentNo == superParentNo))
            //    {
            //        continue;
            //    }

            //    foreach (var text in textList.Where(x => x.SuperParentNo == superParentNo))
            //    {
            //        text.X1 += maxX2;
            //        text.X2 += maxX2;
            //    }

            //    foreach (var rect in textList.Where(x => x.SuperParentNo == superParentNo))
            //    {
            //        rect.X1 += maxX2;
            //        rect.X2 += maxX2;
            //    }

            //    listAdjusted.Add(new NotConnectItemAdjusted() { SuperParentNo = superParentNo });
            //}
        }

        private static void Rect_ItemInitialize(List<MindRow_Text> textList,
            ref List<MindRow_Rect> rectList)
        {
            // X1で昇順にソート。これでRectのZ軸に対処してる。
            textList.Sort((a, b) => a.ChildColumnNo - b.ChildColumnNo);

            foreach (var text in textList)
            {
                var rect = new MindRow_Rect()
                {
                    KeywordNo = text.KeywordNo,
                    Sentence = text.Sentence,
                    //Fill = "#FFFF" + Convert.ToString(255 - (item.Z_No * 15), 16),    // Rectの背景色を黄色ベースで調節。
                    Fill = "#FFFF" + (255 - (text.ChildColumnNo * 15)).ToString("X2"),    // Rectの背景色を黄色ベースで調節。
                    //Fill = "#FF0000",
                    X1 = text.X1 - _Rect_Margin_X,        // 余白は10
                    Y1 = text.Y1 - _Rect_Margin_Y1,
                    Width = text.X2 - text.X1 + (_Rect_Margin_X * 2),  // 余白は10
                    Height = text.Y2 - text.Y1 + _Rect_Margin_Y2 // 余白は10
                };

                rectList.Add(rect);
            }
        }

        private static void Line_ItemInitialize(List<MindItemRow> mindItemList, List<MindRelationRow> mindRelationRows, 
            List<MindRow_Text> textList, ref List<MindRow_Line> lineList)
        {
            var lineCnt = 1;
            foreach (var relationItem in mindRelationRows.Where(x => x.RelationType == RelationType.接続))
            {
                var leftItem = textList.First(x => x.KeywordNo == relationItem.Left_KeywordNo);
                var rightItem = textList.First(x => x.KeywordNo == relationItem.Right_KeywordNo);
                //var superParentNo_Left = textList.First(x => x.No == relationItem.KeywordNo_Left).SuperParentNo;
                //var superParentNo_Right = textList.First(x => x.No == relationItem.KeywordNo_Right).SuperParentNo;
                var superParent_Left = textList.First(x => x.SuperParentNo == leftItem.SuperParentNo);
                var superParent_Right = textList.First(x => x.SuperParentNo == rightItem.SuperParentNo);

                if (leftItem.SuperParentNo == rightItem.SuperParentNo)
                {
                    // SuperParentが同じ。1つのSuperParent内で線を引く。線は3点。

                    //var line = new MindRow_PolyLine()
                    //{
                    //    No_Left = relationItem.LeftItemNo,
                    //    SuperParentNo_Left = superParent_Left.SuperParentNo,
                    //    No_Right = relationItem.RightItemNo,
                    //    SuperParentNo_Right = superParent_Right.SuperParentNo,
                    //    Points =
                    //        (leftItem.X2) + "," + (leftItem.Y1 + 25) + " " +
                    //        (leftItem.X2 + 30) + "," + (leftItem.Y1 + 25) + " " +
                    //        (rightItem.X2 + 30) + "," + (rightItem.Y1 + 25) + " " +
                    //        (rightItem.X2) + "," + (rightItem.Y1 + 25)
                    //};
                    //polyLineList.Add(line);

                    // Y1座標を元に、左アイテムと右アイテムのどちらが上にあるか判定し、Lineアイテム3点のどれになにが当たるか判定する。
                    // Y1が小さい方が上にある。
                    MindRow_Text upperText;
                    MindRow_Text lowerText;
                    if (leftItem.Y1 < rightItem.Y1)
                    {
                        // 左アイテムが上にある
                        upperText = leftItem;
                        lowerText = rightItem;
                    }
                    else
                    {
                        // 右アイテムが上にある
                        upperText = rightItem;
                        lowerText = leftItem;
                    }

                    // 左アイテムと右アイテムとで、x2が大きい方を、縦線の基準にする。
                    int maxX2 = 0;
                    if (lowerText.X2 < upperText.X2)
                    {
                        // 上アイテムの方が右側にせり出してる
                        maxX2 = upperText.X2;
                    }
                    else
                    {
                        // 下アイテムの方が右側にせり出してる
                        maxX2 = lowerText.X2;
                    }

                    // 後続の計算が楽になるように、子アイテム数をセット
                    var itemCount_Left = textList.Count(x => x.SuperParentNo == superParent_Left.SuperParentNo);
                    var itemCount_Right = textList.Count(x => x.SuperParentNo == superParent_Right.SuperParentNo);

                    lineList.Add(new MindRow_Line()
                    {
                        Cnt = lineCnt,
                        Type = LineType.SuperParentが同じ,
                        No_Left = upperText.KeywordNo,
                        SuperParentNo_Left = upperText.SuperParentNo,
                        ItemCount_Left = itemCount_Left,
                        No_Right = lowerText.KeywordNo,
                        SuperParentNo_Right = lowerText.SuperParentNo,
                        ItemCount_Right = itemCount_Right,
                        PolyLineNo = PolyLineNo.A_始点から中間点,
                        X1 = upperText.X2 + _Rect_Margin_X,
                        X2 = maxX2 + _Rect_Margin_X + _Line_Margin_X_SuperParentが同じ,
                        Y1 = upperText.Y1 + _Line_Margin_Y,
                        Y2 = upperText.Y1 + _Line_Margin_Y
                    });

                    var lineCenter = new MindRow_Line()
                    {
                        Cnt = lineCnt,
                        Type = LineType.SuperParentが同じ,
                        No_Left = upperText.KeywordNo,
                        SuperParentNo_Left = upperText.SuperParentNo,
                        ItemCount_Left = itemCount_Left,
                        No_Right = lowerText.KeywordNo,
                        SuperParentNo_Right = lowerText.SuperParentNo,
                        ItemCount_Right = itemCount_Right,
                        PolyLineNo = PolyLineNo.B_中間点から中間点,
                        X1 = maxX2 + _Rect_Margin_X + _Line_Margin_X_SuperParentが同じ,
                        X2 = maxX2 + _Rect_Margin_X + _Line_Margin_X_SuperParentが同じ,
                        Y1 = upperText.Y1 + 25,
                        Y2 = lowerText.Y1 + 25
                    };

                    if (!string.IsNullOrEmpty(relationItem.Sentence))
                    {
                        lineCenter.HaveSentence = true;
                        lineCenter.Sentence = relationItem.Sentence;
                        lineCenter.SentenceWidth = mindItemList.First(x => x.KeywordNo == relationItem.KeywordNo_Sentence).SentenceWidth;
                        lineCenter.SentenceHeight = _Line_Sentence_Height;
                    }
                    lineList.Add(lineCenter);

                    lineList.Add(new MindRow_Line()
                    {
                        Cnt = lineCnt,
                        Type = LineType.SuperParentが同じ,
                        No_Left = upperText.KeywordNo,
                        SuperParentNo_Left = upperText.SuperParentNo,
                        ItemCount_Left = itemCount_Left,
                        No_Right = lowerText.KeywordNo,
                        SuperParentNo_Right = lowerText.SuperParentNo,
                        ItemCount_Right = itemCount_Right,
                        PolyLineNo = PolyLineNo.C_中間点から終点,
                        X1 = lowerText.X2 + _Rect_Margin_X,
                        X2 = maxX2 + _Rect_Margin_X + _Line_Margin_X_SuperParentが同じ,
                        Y1 = lowerText.Y1 + _Line_Margin_Y,
                        Y2 = lowerText.Y1 + _Line_Margin_Y
                    });

                    lineCnt++;
                }
                else
                {
                    var line = new MindRow_Line()
                    {
                        Cnt = lineCnt,
                        No_Left = relationItem.Left_KeywordNo,
                        SuperParentNo_Left = superParent_Left.SuperParentNo,
                        ItemCount_Left = textList.Count(x => x.SuperParentNo == superParent_Left.SuperParentNo),
                        No_Right = relationItem.Right_KeywordNo,
                        SuperParentNo_Right = superParent_Right.SuperParentNo,
                        ItemCount_Right = textList.Count(x => x.SuperParentNo == superParent_Right.SuperParentNo),
                        X1 = leftItem.X2 + _Line_Margin_X_Left,
                        Y1 = leftItem.Y1 + _Line_Margin_Y,
                        X2 = rightItem.X1 + _Line_Margin_X_Right,
                        Y2 = rightItem.Y1 + _Line_Margin_Y
                    };


                    if (!string.IsNullOrEmpty(relationItem.Sentence))
                    {
                        line.HaveSentence = true;
                        line.Sentence = relationItem.Sentence;
                        line.SentenceWidth = mindItemList.First(x => x.KeywordNo == relationItem.KeywordNo_Sentence).SentenceWidth;
                        line.SentenceHeight = _Line_Sentence_Height;
                    }

                    lineList.Add(line);

                    lineCnt++;
                }

            }
        }

        private static void Line_ColorAdjustment(ref List<MindRow_Line> lineList)
        {
            foreach (var item in lineList)
            {
                item.Stroke = "#5A" + (255 - (item.Cnt * 5)).ToString("X2") + "5A";    // Lineの色を緑色ベースで調節。
            }
        }





        //private static void Text_アイテムそれぞれが抱えてる子アイテムの階層をセット(ref List<MindRow_Text> textList)
        //{
        //    foreach (var item in textList)
        //    {
        //        // Text Item
        //        item.SuperParentNo = GetSuperParentNo(textList, item.KeywordNo);

        //        // Rect Item
        //        //rectList.First(x => x.No == item.No).SuperParentNo = item.SuperParentNo;
        //    }

        //    //// SuperParentのItemかどうかを明確にする。
        //    //foreach (var item in textList.Where(x => x.No == x.SuperParentNo))
        //    //{
        //    //    item.SuperParent = true;
        //    //}
        //}


        private static void Text_依存_Y1調整_子アイテムのみ(ref List<MindRow_Text> textList)
        {
            foreach (var text in textList)
            {
                var 子Items = textList.Where(x => x.ParentNo == text.KeywordNo);

                // 親アイテムとのY座標が調整されていなかったら調子する。何度もインクリメントされるのは避けてる。
                foreach (var 子Item in 子Items)
                {
                    if (text.Y1 != 子Item.Y1)
                    {
                        continue;
                    }

                    var plusY = text.Y2 + _Margin_Y_Height;

                    子Item.Y1 += plusY;
                    子Item.Y2 += plusY;

                }

            }

            //foreach (var item in mindRelationRow.Where(x => x.RelationType == RelationType.依存))
            //{
            //    var leftItem = textList.First(x => x.No == item.KeywordNo_Left);
            //    var rightItem = textList.First(x => x.No == item.KeywordNo_Right);

            //    // 子Itemを親Itemの右に配置。
            //    rightItem.X1 = leftItem.X1 + _Margin_X_TextItems;
            //}
        }


        private static void 保持している子アイテム数を取得(MindRow_Text item, List<MindRow_Text> textList,
            ref int 保持している全子アイテムの数)
        {
            var 子Items = textList.Where(x => x.ParentNo == item.KeywordNo);

            if (子Items.Count() < 1)
            {
                return;
            }

            保持している全子アイテムの数 += 子Items.Count();

            foreach (var 子Item in 子Items)
            {
                保持している子アイテム数を取得(子Item, textList, ref 保持している全子アイテムの数);
            }
        }

        private static void 保持している子アイテムの階層数を取得(MindRow_Text item, List<MindRow_Text> textList,
            ref int 保持している全子アイテムの階層数)
        {
            var 子Items = textList.Where(x => x.ParentNo == item.KeywordNo);

            if (子Items.Count() < 1)
            {
                return;
            }

            var maxChildColumnNo = 0;
            一番大きいChildColumnNo取得(item, textList, ref maxChildColumnNo);

            保持している全子アイテムの階層数 += 子Items.Count();

            foreach (var 子Item in 子Items)
            {
                保持している子アイテム数を取得(子Item, textList, ref 保持している全子アイテムの階層数);
            }
        }

        private static void 一番大きいChildColumnNo取得(MindRow_Text item, List<MindRow_Text> textList,
            ref int maxChildColumnNo)
        {
            var 子Items = textList.Where(x => x.ParentNo == item.KeywordNo);

            if (子Items.Count() < 1)
            {
                return;
            }

            var childColumnNo = 子Items.First().ChildColumnNo;
            if (maxChildColumnNo < childColumnNo)
            {
                maxChildColumnNo = childColumnNo;
            }

            foreach (var 子Item in 子Items)
            {
                一番大きいChildColumnNo取得(子Item, textList, ref maxChildColumnNo);
            }
        }

        // 子、孫、ひ孫と、配下にあるアイテム全てで、最もX2が大きいアイテムのX2を返す。再帰。
        private static int? Max_ChildX2(MindRow_Text item, List<MindRow_Text> textList)
        {
            var 子Items = textList.Where(x => x.ParentNo == item.KeywordNo);

            if (子Items.Count() < 1)
            {
                // 子アイテムが無いので調整不要
                return null;
            }

            var maxX2 = item.X2;
            foreach (var 子Item in 子Items)
            {
                // 子アイテムの中に、孫アイテムも持っているのがあれば、再帰で、その中から最大のX2を取得する。
                var x2 = Max_ChildX2(子Item, textList);

                if (maxX2 < x2)
                {
                    // 孫アイテムのX2の方が大きければ、それを採用。
                    maxX2 = (int)x2;
                }

                if (maxX2 < 子Item.X2)
                {
                    // 孫アイテムが無い場合を含め、子アイテムのX2の方が大きければ、それを採用。
                    maxX2 = 子Item.X2;
                }
            }

            return maxX2;
        }




        private static MindRow_Text Y1が一番下になるItemを探索して返す_再帰(List<MindRow_Text> textList, IEnumerable<MindRow_Text> ParentItems)
        {
            MindRow_Text parentItem一番下 = ParentItems.First();

            // 一番下にある親Itemを探索
            foreach (var parentItem in ParentItems)
            {
                if (parentItem一番下.Y1 > parentItem.Y1)
                {
                    parentItem一番下 = parentItem;
                }
            }

            var childItem = textList.Where(x => x.ParentNo == parentItem一番下.KeywordNo);
            if (childItem.Count() < 1)
            {
                // 子が無いので、一番下の親を返す。
                return parentItem一番下;
            }
            else
            {
                // 一番下にある親Itemの子Itemの内、一番下にある子Itemを再帰で探索
                return Y1が一番下になるItemを探索して返す_再帰(textList, childItem);
            }
        }


        //private static void Text_SetHierarchy(ref List<MindRow_Text> textList)
        //{
        //    foreach (var superParentText in textList.Where(x => x.SuperParent))
        //    {
        //        //short zNo = 1;
        //        //short maxColumnNo = 1;
        //        foreach (var item in textList
        //            .Where(x => x.SuperParentNo == superParentText.SuperParentNo)
        //            .OrderBy(x => x.ChildColumnNo))
        //        {
        //            item.Z_No = item.ChildColumnNo - superParentText.ChildColumnNo;
        //            //zNo++;
        //        }
        //    }
        //}

        // y座標の重複を解消。
        // 親アイテム間で、Y軸の重複があれば重複を解消する。
        private static void Text_PositionCalculation_DuplicationY_SuperParent以外(ref List<MindRow_Text> textList)
        {
            foreach (var item in textList)
            {
                var superParent = textList.First(x => x.KeywordNo == item.SuperParentNo);

                // 自分以外のsuperParentを対象にループ処理する
                foreach (var superParent_item所属以外 in textList.Where(x => 
                    x.KeywordNo != x.SuperParentNo &&   // SuperParent以外
                    item.KeywordNo != x.KeywordNo))     // 自分以外
                {
                    if (superParent.X1 == superParent_item所属以外.X1 && superParent.Y1 == superParent_item所属以外.Y1)
                    {
                        // 重複していなければ何もしない。
                        continue;
                    }

                    // X1Y1が重複しているSuperParentに所属しているアイテム全てを移動する
                    foreach (var item2 in textList.Where(x => x.SuperParentNo == superParent_item所属以外.KeywordNo))
                    {
                        item2.Y1 += superParent.Y2 + _Margin_Y_Height;
                        item2.Y2 += superParent.Y2 + _Margin_Y_Height;
                    }
                }


                //var 重複List = textList.Where(x => x.X1 == superParent.X1 && x.Y1 == superParent.Y1 && );

                //if (重複List.Count() < 1)
                //{
                //    continue;
                //}

                //var left_Y2_Max = .Max(x => x.Y2);

                //superParent.Y1 += left_Y2_Max;
                //superParent.Y2 += left_Y2_Max;

                //foreach (var childitem in textList.Where(x => x.SuperParentNo == ))
                //{

                //}
            }

            //var Duplication1 = false;
            //var Duplication2 = false;

            //while (true)
            //{
            //    //textList

            //    foreach (var text in textList)
            //    {
            //        if (text.ParentNo != null)
            //        {
            //            // 親を持っている子アイテムは対象外
            //            continue;
            //        }

            //        var duplicationText = textList.Where(x => x.ParentNo == null)
            //            .Where(x => x.SuperParentNo != text.No)
            //            .Where(x => text.Y1 == x.Y1).ToList();

            //        if (duplicationText.Count() < 1)
            //        {
            //            // 重複無し。
            //            continue;
            //        }

            //        Duplication1 = true;


            //        // 自分の配下に居る子孫を全て返す。
            //        var diff_Y = 0;
            //        var diff_columnNo = 1; // +1は自分自身。
            //        var exclusionText = GetChilds(textList, text.No);
            //        if (exclusionText.Count() > 0)
            //        {
            //            foreach (var exclusion in exclusionText)
            //            {
            //                // 自分の配下に居る子孫は再配置の対象外にする。
            //                duplicationText.RemoveAll(x => x.No == exclusion.No);
            //            }

            //            diff_Y = exclusionText.Max(x => x.Y1) - text.Y1;
            //            diff_columnNo = exclusionText.Max(x => x.ChildColumnNo) - text.ChildColumnNo + 1; // +1は自分自身。
            //        }


            //        // 再配置。
            //        foreach (var duplicat in duplicationText)
            //        {
            //            duplicat.Y1 = duplicat.Y1 + 70 + diff_Y + (diff_columnNo * 20);  // Item間のY差70、親の余白20。
            //            duplicat.Y2 = duplicat.Y1 + duplicat.Height;
            //        }
            //    }

            //    // 重複が有るかの判定は、2回連続で重複
            //    Duplication2 = Duplication1;
            //    Duplication1 = false;
            //    if (!Duplication1 && !Duplication2)
            //    {
            //        break;
            //    }
            //}
        }

        // y座標の重複を解消。
        // 親アイテム間で、Y軸の重複があれば重複を解消する。
        private static void Text_PositionCalculation_DuplicationY_SuperParent(ref List<MindRow_Text> textList)
        {
            var Duplication1 = false;
            var Duplication2 = false;
            while (true)
            {
                foreach (var item in textList)
                {
                    var superParent = textList.First(x => x.KeywordNo == item.SuperParentNo);

                    var text重複 = textList.Where(x => superParent.X1 == x.X1 && superParent.Y1 == x.Y1 && superParent.KeywordNo != x.KeywordNo);

                    if (text重複.Count() < 1)
                    {
                        // 重複していなければ何もしない。
                        continue;
                    }

                    var maxY2 = text重複.Max(x => x.Y2);
                    var moveY = maxY2 - superParent.Y1 + _SuperParent間の余白;

                    // X1Y1が重複しているSuperParentに所属しているアイテム全てを移動する
                    foreach (var item2 in textList.Where(x => x.SuperParentNo == superParent.KeywordNo))
                    {
                        item2.Y1 += moveY;
                        item2.Y2 += moveY;
                    }

                    Duplication1 = true;
                }

                // 2回連続で重複を、重複が有るかの判定にする
                Duplication2 = Duplication1;
                Duplication1 = false;
                if (!Duplication1 && !Duplication2)
                {
                    break;
                }
            }
        }

        // 自分の配下に居る子アイテムを、再帰的に探索して、子アイテム全て返す。
        private static List<MindRow_Text> GetChilds(List<MindRow_Text> textList, long parentNo)
        {
            var childs = new List<MindRow_Text>();
            var grandchild = new List<MindRow_Text>();

            childs = textList.Where(x => x.ParentNo == parentNo).ToList();

            foreach (var child in childs)
            {
                grandchild.AddRange(GetChilds(textList, child.KeywordNo));
            }

            grandchild.AddRange(childs);

            return grandchild;
        }

        // 子Itemsの中で最も大きいWidthを返す。
        private static void GetMaxY2(List<MindRow_Text> textList, MindRow_Text mine, 
            ref int maxY2)
        {
            var childs = textList.Where(x => x.ParentNo == mine.KeywordNo);

            if (childs.Count() < 1)
            {
                // 子無し。最も下位のItem。

                if (maxY2 < mine.Y2)
                {
                    maxY2 = mine.Y2;
                }

                return;
            }

            // 子持ち。
            foreach (var child in childs)
            {
                if (maxY2 < child.Y2)
                {
                    // 子が最大Y1を超えてる。
                    maxY2 = child.Y2;
                }

                // 孫も処理。
                GetMaxY2(textList, child, ref maxY2);
            }
        }


        // 子Itemの最も親に当たるItemのNoを返す。
        private static long GetSuperParentNo(List<MindRow_Text> textList, long no)
        {
            long childNo = no;
            MindRow_Text item;

            do
            {
                item = textList.First(x => x.KeywordNo == childNo);

                if (item.ParentNo == null)
                {
                    break;
                }

                childNo = (int)item.ParentNo;
            }
            while (item.ParentNo != null);

            return item.KeywordNo;
        }



        //private static void Rect_PositionCalculation(ref List<MindRow_Rect> rectList)
        //{
        //    while (rectList.Any(x => x.Adjusted == false))
        //    {
        //        var rect = rectList.Where(x => x.Adjusted == false).OrderByDescending(x => x.ColumnNo).First();


        //        //var width = item.Width;
        //        //int x1 = item.X1;
        //        //int y1 = item.Y1;
        //        //var inversionColumnNoList = new List<InversionColumnNo>();

        //        //GetMaxWidth(rectList, item, ref x1, ref y1, ref width, ref inversionColumnNoList);

        //        //SetInversionColumnNoList(ref inversionColumnNoList);
        //        //var inversionColumnNo = inversionColumnNoList.First(x => x.ColumnNo == item.ColumnNo).InversionNo;

        //        //var rect = rectList.First(x => x.No == item.No);
        //        //rect.Width = ((x1 - item.X1) * inversionColumnNo) + width + 20;
        //        //rect.Height = y1 - item.Y1 + 50 + (20 * inversionColumnNo);

        //        if (rectList.Any(x => x.ParentNo == rect.No))
        //        {
        //            // X軸

        //            var child_Max_X2 = rectList.Where(x => x.ParentNo == rect.No).Max(x => x.X2);

        //            if (child_Max_X2 > rect.X2)
        //            {
        //                // 親より子の方が大きい場合、子のサイズを加味する。
        //                // 余白10
        //                rect.Width = child_Max_X2 - rect.X1 + _Margin_Y_BetweenRectItems;
        //            }
                    
        //            if ((rect.X2 - child_Max_X2) < _Margin_Y_BetweenRectItems)
        //            {
        //                // 親より子の方が小さくても、余白ほどの差が無い場合、余白10を足す。
        //                // 余白10
        //                rect.Width += _Margin_Y_BetweenRectItems;
        //            }

        //            rect.X2 = rect.X1 + rect.Width;


        //            // Y軸

        //            var child_Max_Y2 = rectList.Where(x => x.ParentNo == rect.No).Max(x => x.Y2);

        //            if (child_Max_Y2 > rect.Y2)
        //            {
        //                // 親より子の方が大きい場合、子のサイズを加味する。
        //                // 余白10
        //                rect.Height = child_Max_Y2 - rect.Y1 + _Margin_Y_BetweenRectItems;
        //            }

        //            if ((rect.Y2 - child_Max_Y2) < _Margin_Y_BetweenRectItems)
        //            {
        //                // 親より子の方が小さくても、余白ほどの差が無い場合、余白10を足す。
        //                // 余白10
        //                rect.Height += _Margin_Y_BetweenRectItems;
        //            }

        //            rect.Y2 = rect.Y1 + rect.Height;
        //        }

        //        rect.Adjusted = true;

        //        //int maxY;
        //        //if (rectList.Any(x => x.ParentNo == item.No))
        //        //{
        //        //    maxY = rectList.Where(x => x.ParentNo == item.No).Max(x => x.Y2);

        //        //    item.Height = maxY - item.Y1;
        //        //}

        //    }
        //}

        // 子Itemsの中で最も大きいWidthを返す。
        //private static void GetMaxWidth(List<MindRow_Rect> rectList, MindRow_Rect mine,
        //    ref int maxX1, ref int maxY1, ref int maxWidth, ref List<InversionColumnNo> inversionColumnNoList)
        //{
        //    var childs = rectList.Where(x => x.ParentNo == mine.No);

        //    if (childs.Count() < 1)
        //    {
        //        // 子無し。最も下位のItem。

        //        if (maxX1 < mine.X1)
        //        {
        //            maxX1 = mine.X1;
        //        }

        //        if (maxY1 < mine.Y1)
        //        {
        //            maxY1 = mine.Y1;
        //        }

        //        if (maxWidth < mine.Width)
        //        {
        //            maxWidth = mine.Width;
        //        }

        //        if (!inversionColumnNoList.Any(x => x.ColumnNo == mine.ColumnNo))
        //        {
        //            inversionColumnNoList.Add(new InversionColumnNo() { ColumnNo = mine.ColumnNo });
        //        }

        //        return;
        //    }

        //    // 子持ち。
        //    foreach (var child in childs)
        //    {
        //        if (maxX1 < child.X1)
        //        {
        //            // 子が最大X1を超えてる。
        //            maxX1 = child.X1;
        //        }

        //        if (maxY1 < child.Y1)
        //        {
        //            // 子が最大Y1を超えてる。
        //            maxY1 = child.Y1;
        //        }

        //        if (maxWidth < child.Width)
        //        {
        //            // 子が最大幅を超えてる。
        //            maxWidth = child.Width;
        //        }

        //        if (!inversionColumnNoList.Any(x => x.ColumnNo == mine.ColumnNo))
        //        {
        //            inversionColumnNoList.Add(new InversionColumnNo() { ColumnNo = mine.ColumnNo });
        //        }

        //        // 孫も処理。
        //        GetMaxWidth(rectList, child, ref maxX1, ref maxY1, ref maxWidth, ref inversionColumnNoList);
        //    }
        //}


        private static void Line_SentenceSpace_Approach(ref List<MindRow_Text> textList, ref List<MindRow_Line> lineList)
        {
            const byte _Space = 20;

            foreach (var line in lineList.Where(x => x.HaveSentence))
            {
                var halfWidth = line.SentenceWidth / 2;

                //var center_X = line.X1 + ((line.X2 - line.X1) / 2);
                var leftX = textList.First(x => x.KeywordNo == line.SuperParentNo_Left);
                var rightX = textList.First(x => x.KeywordNo == line.SuperParentNo_Right);
                var center_X = leftX.X2 + ((rightX.X1 - leftX.X2) / 2);

                // 別の親に接続している場合、余白を確保する。
                if (line.Type == LineType.SuperParentが違う)
                {
                    var space = 0;
                    var maxX2 = textList.Where(x => x.KeywordNo == line.SuperParentNo_Left).Max(x => x.X2);
                    if (maxX2 > (center_X - halfWidth - _Space))
                    {
                        // Line左側分の、余白を確保する。
                        space += maxX2 - (center_X - halfWidth - _Space);
                    }

                    var minX1 = textList.Where(x => x.KeywordNo == line.SuperParentNo_Right).Min(x => x.X1);
                    if (minX1 < (center_X + halfWidth + _Space))
                    {
                        // Line右側分の、余白を確保する。
                        space += (center_X + halfWidth + _Space) - minX1;
                    }

                    // 左Itemが複数のItemに接続している場合、左側以外のItemは全て、右側に移動する。
                    foreach (var item in textList.Where(x => x.X1 >= minX1))
                    {
                        item.X1 += space;
                        item.X2 += space;
                        //item.X2 = item.X1 + item.Width;
                    }

                    // 調整対象のLineが接続している左アイテムに、接続しているLineがあれば、全て調整する
                    foreach (var line2 in lineList.Where(x => x.No_Left == line.No_Left))
                    {
                        line2.X2 += space;
                    }

                    //// lineの右側を揃える。
                    //foreach (var linItem in lineList
                    //    .Where(x => x.Type == LineType.Line)
                    //    //.Where(x => item.No == x.SuperParentNo_Right)
                    //    )
                    //{
                    //    linItem.X2 += space;
                    //}

                    // Line（自分）の右端を調整。
                    //line.X2 += space;


                    //// Line（自分以外）で、変更した位置より、完全に右側にあるLineを調整。
                    //foreach (var lineItem in lineList
                    //    //.Where(x => x.Type == LineType.PolyLine)
                    //    .Where(x => x.No_Left != line.No_Left || x.No_Right != line.No_Right)
                    //    .Where(x => x.X1 >= minX1))
                    //{
                    //    lineItem.X1 += space;
                    //    lineItem.X2 += space;
                    //}

                    //// Line（自分以外）で、変更した位置より、右側のItemだけが、右側にあるLineを調整。
                    //foreach (var lineItem in lineList
                    //    //.Where(x => x.Type == LineType.PolyLine)
                    //    .Where(x => x.No_Left != line.No_Left || x.No_Right != line.No_Right)
                    //    .Where(x => x.X1 < minX1 && minX1 <= x.X2))
                    //{
                    //    lineItem.X2 += space;
                    //}
                }
            }
        }

        private static void Line_Sentence_Approach(ref List<MindRow_Text> textList, ref List<MindRow_Line> lineList)
        {
            foreach (var line in lineList.Where(x => x.HaveSentence && x.Type == LineType.SuperParentが違う))
            {
                var halfWidth = line.SentenceWidth / 2;
                var halfHeight = line.SentenceHeight / 2;

                var leftX = textList.First(x => x.KeywordNo == line.SuperParentNo_Left).X2;
                var rightX = textList.First(x => x.KeywordNo == line.SuperParentNo_Right).X1;

                var center_X = leftX + ((rightX - leftX) / 2);
                var center_Y = line.Y1 + ((line.Y2 - line.Y1) / 2);

                line.SentenceRect_X1 = center_X - halfWidth;
                line.SentenceRect_Y1 = center_Y - halfHeight;
                line.SentenceRect_X2 = center_X + halfWidth;
                line.SentenceRect_Y2 = center_Y + halfHeight;

                line.SentenceText_X1 = center_X - halfWidth + 15;   // 15は余白
                line.SentenceText_Y1 = center_Y - halfHeight + 20;  // 20は余白

                //if (line.PolyLineNo != PolyLineNo.B)
                //{
                //    line.X1 = textList.First(x => x.No == line.No_Left).X2;
                //}
            }

            foreach (var line in lineList.Where(x => x.HaveSentence && x.Type == LineType.SuperParentが同じ && x.PolyLineNo == PolyLineNo.B_中間点から中間点))
            {
                var halfWidth = line.SentenceWidth / 2;
                var halfHeight = line.SentenceHeight / 2;

                var center_X = line.X1;
                var center_Y = line.Y1 + ((line.Y2 - line.Y1) / 2);

                line.SentenceRect_X1 = center_X - halfWidth;
                line.SentenceRect_Y1 = center_Y - halfHeight;
                line.SentenceRect_X2 = center_X + halfWidth;
                line.SentenceRect_Y2 = center_Y + halfHeight;

                line.SentenceText_X1 = center_X - halfWidth + 15;   // 15は余白
                line.SentenceText_Y1 = center_Y - halfHeight + 20;  // 20は余白
            }
        }

        private static void PolyLine_Sentence_DuplicateApproach(ref List<MindRow_Line> lineList)
        {
            var polylines_Center = lineList.Where(x => x.HaveSentence && x.Type == LineType.SuperParentが同じ && x.PolyLineNo == PolyLineNo.B_中間点から中間点);
            foreach (var line in polylines_Center)
            {
                var duplicateLines = lineList
                    .Where(x => x.HaveSentence && x.Type == LineType.SuperParentが同じ && x.PolyLineNo == PolyLineNo.B_中間点から中間点)
                    .Where(x => !(x.No_Left == line.No_Left && x.No_Right == line.No_Right))    // 自分以外
                    .Where(x => line.SentenceRect_Y1 <= x.SentenceRect_Y1 && x.SentenceRect_Y1 <= line.SentenceRect_Y2);

                if (duplicateLines.Count() < 1)
                {
                    // 重複するlineRectは無い。
                    continue;
                }

                // 重複するlineRectが有る。
                var diff = 0;
                foreach (var duplicateLine in duplicateLines)
                {
                    diff = line.SentenceRect_Y2 - duplicateLine.SentenceRect_Y1 + 10;   // 10は余白
                    duplicateLine.SentenceRect_Y1 += diff;
                    duplicateLine.SentenceText_Y1 += diff;
                }
            }
        }

        private static void Line_Sentence_TiltApproach(List<MindRow_Text> textList, ref List<MindRow_Line> lineList)
        {
            var tiltLines = lineList
                .Where(x => x.HaveSentence && x.Type == LineType.SuperParentが違う)
                .Where(x => x.Y1 != x.Y2);

            foreach (var line in tiltLines)
            {
                // 大元Lineの底辺。
                var line_Base = Math.Abs(line.X2 - line.X1);

                // 大元Lineの高さ。
                var line_Height = Math.Abs(line.Y2 - line.Y1);

                // 大元Lineの傾き角度。
                var theta = CalcAngle(line_Base, line_Height);

                var superParentRect_Left = textList.First(x => x.KeywordNo == line.SuperParentNo_Left);
                var superParentRect_Right = textList.First(x => x.KeywordNo == line.SuperParentNo_Right);
                var rect_Left = textList.First(x => x.KeywordNo == line.No_Left);

                // Left SuperParent と Left Item の底辺の差。
                var baseLeft = superParentRect_Left.X2 - rect_Left.X2;

                // Left SuperParent と Left Item の、／（斜線）高さの差。
                var heightLeft = CalcHeight(theta, baseLeft);

                // Line 左端 と Right Item の底辺の差。
                var baseRight = superParentRect_Right.X1 - line.X1;

                // Left SuperParent と Left Item の、／（斜線）高さの差。
                var heightRight = CalcHeight(theta, baseRight);

                // Sentence Rect の、高さの半分。
                var sentenceHeightHalf = line.SentenceHeight / 2;

                if (line.Y1 < line.Y2)
                {
                    var heightDiffHalf = (int)((heightRight - heightLeft) / 2);
                    var SentenceCenter_Y = (int)(line.Y1 + heightLeft + heightDiffHalf);

                    line.SentenceRect_Y1 = SentenceCenter_Y - sentenceHeightHalf;
                    line.SentenceRect_Y2 = SentenceCenter_Y - sentenceHeightHalf + line.SentenceHeight;
                    line.SentenceText_Y1 = SentenceCenter_Y - sentenceHeightHalf + 20;   // 20は、SentenceRectとSentenceTextとの余白。
                }
                else
                {
                    var heightDiffHalf = (int)((heightRight - heightLeft) / 2);
                    var SentenceCenter_Y = (int)(line.Y1 - heightLeft - heightDiffHalf);

                    //sentenceRect_Y1 = 882 - 15;
                    //sentenceRect_Y2 = 882 - 15;
                    //sentenceText_Y1 = 882 + 5;
                    line.SentenceRect_Y1 = SentenceCenter_Y - sentenceHeightHalf;
                    line.SentenceRect_Y2 = SentenceCenter_Y - sentenceHeightHalf + line.SentenceHeight;
                    line.SentenceText_Y1 = SentenceCenter_Y - sentenceHeightHalf + 20;   // 20は、SentenceRectとSentenceTextとの余白。
                }
            }

        }

        /*
         * 底辺と高さから角度を求める  
         * @_base 底辺 
         * @height 高さ 
         * @return 角度 
         */
        private static double CalcAngle(double _base, double height)
        {
            return Math.Atan(height / _base) * 180 / Math.PI;
        }

        /*
         * 角度と底辺から高さを求める
         * @angle 角度
         * @_base 底辺
         * @return 斜辺
         */
        private static double CalcHeight(double angle, double _base)
        {
            return _base * Math.Tan(Math.PI * angle / 180);
        }


        // Lineの関係性を元にTextアイテムの配置を調整する
        private static void Line_保有ITem数が少ない方のY座標を接続先に近付ける(List<MindRow_Line> lineList, ref List<MindRow_Text> textList)
        {
            var line調整済 = new List<LineAdjusted>();

            foreach (var line in lineList)
            {
                if (line.Type == LineType.SuperParentが同じ)
                {
                    // SuperParentが同じ
                    continue;
                }

                //if (line.X1 <= line.X2)
                //{
                //    // 右アイテムが左アイテムと同じか、左アイテムより下にあったら調整不要。
                //    continue;
                //}

                List<MindRow_Text> textList_Tmp;
                int move;
                long adjustedSuperParentNo;

                // 保有ITem数が少ない方の位置を調整する。
                if (line.ItemCount_Left < line.ItemCount_Right)
                {
                    // 左側を調整する。

                    move = line.Y2 - line.Y1;
                    if (move < 1)
                    {
                        // 0はスキップ。マイナスは位置を戻す処理になりやり過ぎなのでスキップ。
                        continue;
                    }

                    textList_Tmp = textList.Where(x => x.SuperParentNo == line.SuperParentNo_Left).ToList();

                    line.Y1 += move;

                    adjustedSuperParentNo = (long)line.SuperParentNo_Left;
                }
                else
                {
                    // 右側を調整する。

                    move = line.Y1 - line.Y2;
                    if (move < 1)
                    {
                        // 0はスキップ。マイナスは位置を戻す処理になりやり過ぎなのでスキップ。
                        continue;
                    }

                    textList_Tmp = textList.Where(x => x.SuperParentNo == line.SuperParentNo_Right).ToList();

                    line.Y2 += move;

                    adjustedSuperParentNo = (long)line.SuperParentNo_Right;
                }

                if (!line調整済.Any(x => x.SuperParentNo == adjustedSuperParentNo))
                {
                    // 未調整。

                    foreach (var item in textList_Tmp)
                    {
                        item.Y1 += move;
                        item.Y2 += move;
                    }

                    line調整済.Add(new LineAdjusted() { SuperParentNo = adjustedSuperParentNo });
                }
            }
        }

        private static void SuperParentのX1X2Y1Y2を最新の状態に更新(
            List<MindRow_Text> textList, ref List<SuperParent> superParentList)
        {
            foreach (var superParent in superParentList)
            {
                var text = textList.First(x => x.KeywordNo == superParent.KeywordNo);

                superParent.X1 = text.X1;
                superParent.X2 = text.X2;
                superParent.Y1 = text.Y1;
                superParent.Y2 = text.Y2;
            }
        }

        private static void Line_保有ITem数が少ない方のY座標を接続先に近付ける_Y軸の重複を解消(
            List<SuperParent> superParentList, List<MindRow_Line> lineList, ref List<MindRow_Text> textList)
        {
            // 後の計算で使用する、全lineアイテムにセットされている、SuperParentNoのリストを作成する
            var lineSuperParentNoList = new List<long>();
            foreach (var line in lineList)
            {
                if (!lineSuperParentNoList.Any(x => x == line.SuperParentNo_Left))
                {
                    lineSuperParentNoList.Add((long)line.SuperParentNo_Left);
                }

                if (!lineSuperParentNoList.Any(x => x == line.SuperParentNo_Right))
                {
                    lineSuperParentNoList.Add((long)line.SuperParentNo_Right);
                }
            }

            // 何らかの接続関係が有るSuperParentListを作成
            var superParentList_接続関係有り = new List<SuperParent>();
            foreach (var keywordNo in lineSuperParentNoList)
            {
                superParentList_接続関係有り.Add(superParentList.First(x => x.KeywordNo == keywordNo));
            }

            foreach (var superParent in superParentList_接続関係有り)
            {
                var superParentList重複 = superParentList_接続関係有り
                    .Where(x => x.SuperParent列No == superParent.SuperParent列No)
                    .Where(x => x.KeywordNo != superParent.KeywordNo)
                    .Where(x => (superParent.Y1 <= x.Y1 && x.Y1 <= superParent.Y2) || (superParent.Y1 <= x.Y2 && x.Y2 <= superParent.Y2));

                // 下にある方を、更に下に移動する。
                foreach (var superParent重複 in superParentList重複)
                {
                    if (superParent.Y1 < superParent重複.Y1)
                    {
                        // superParent より superParent重複 の方が下にある。

                        var y移動 = superParent重複.Y1 - superParent.Y1 + _SuperParent間の余白;

                        // 重複しているSuperParentに所属しているアイテム全てを移動する
                        foreach (var item in textList.Where(x => x.SuperParentNo == superParent重複.KeywordNo))
                        {
                            item.Y1 += y移動;
                            item.Y2 += y移動;
                        }

                        // superParentのY1Y2も連動させる
                        superParent重複.Y1 += y移動;
                        superParent重複.Y2 += y移動;
                    }
                    else
                    {
                        // superParent重複 より superParent の方が下にある。

                        var y移動 = superParent重複.Y2 - superParent.Y1 + _SuperParent間の余白;

                        // 重複しているSuperParentに所属しているアイテム全てを移動する
                        foreach (var item in textList.Where(x => x.SuperParentNo == superParent.KeywordNo))
                        {
                            item.Y1 += y移動;
                            item.Y2 += y移動;
                        }

                        // superParentのY1Y2も連動させる
                        superParent.Y1 += y移動;
                        superParent.Y2 += y移動;
                    }
                }
            }
        }

        private static void Line_PositionApproach_Y座標(List<MindRow_Text> textList, ref List<MindRow_Line> lineList)
        {
            var line調整済 = new List<LineAdjusted>();

            foreach (var line in lineList)
            {
                if (line.Type == LineType.SuperParentが同じ)
                {
                    continue;
                }

                var leftItem = textList.First(x => x.KeywordNo == line.No_Left);
                var rightItem = textList.First(x => x.KeywordNo == line.No_Right);

                line.Y1 = leftItem.Y1 + _Line_Margin_Y;
                line.Y2 = rightItem.Y1 + _Line_Margin_Y;
            }
        }

        private static void PolyLine_X_PositionApproach(ref List<MindRow_Line> lineList)
        {
            var polyLineList = new List<PolyLine>();

            foreach (var group in lineList
                .Where(x => x.Type == LineType.SuperParentが同じ)
                //.GroupBy(x => new { No_Left = x.No_Left, No_Right = x.No_Right, SuperParentNo_Left = x.SuperParentNo_Left, SuperParentNo_Right = x.SuperParentNo_Right })
                .GroupBy(x => new { No_Left = x.No_Left, No_Right = x.No_Right }))
            {
                var polyLine = new PolyLine()
                {
                    No_Left = group.Key.No_Left,
                    No_Right = group.Key.No_Right,
                };

                foreach (var item in lineList.Where(x => x.No_Left == group.Key.No_Left && x.No_Right == group.Key.No_Right)
                    .Where(x => x.Type == LineType.SuperParentが同じ)
                    .OrderBy(x => x.PolyLineNo))
                {
                    if (item.PolyLineNo == PolyLineNo.A_始点から中間点)
                    {
                        polyLine.SuperParentNo_Left = item.SuperParentNo_Left;
                        polyLine.ItemCount_Left = item.ItemCount_Left;
                        polyLine.SuperParentNo_Right = item.SuperParentNo_Right;
                        polyLine.ItemCount_Right = item.ItemCount_Right;
                        polyLine.X1 = item.X1;
                        polyLine.Y1 = item.Y1;
                        polyLine.X2 = item.X2;
                        polyLine.Y2 = item.Y2;
                    }
                    else if (item.PolyLineNo == PolyLineNo.B_中間点から中間点)
                    {
                        continue;
                    }
                    else if (item.PolyLineNo == PolyLineNo.C_中間点から終点)
                    {
                        polyLine.X3 = item.X2;
                        polyLine.Y3 = item.Y2;
                        polyLine.X4 = item.X1;
                        polyLine.Y4 = item.Y1;
                    }
                }

                polyLineList.Add(polyLine);
            }


            foreach (var polyLine in polyLineList)
            {
                foreach (var duplicate in polyLineList
                    .Where(x => x.No_Left != polyLine.No_Left && x.No_Right != polyLine.No_Right)   // 自分以外
                    .Where(x => x.X1 <= polyLine.X2 && polyLine.X2 <= x.X2)    // 縦線が被る可能性がある
                    .Where(x => polyLine.Y1 <= x.Y1 && x.Y4 <= polyLine.Y4)    // 縦線が完全に被ってる;
                    )
                {
                    polyLine.X2 += (duplicate.X2 - polyLine.X2) + 20;
                    polyLine.X3 = polyLine.X2;
                }
            }

            foreach (var polyLine in polyLineList)
            {
                var lineA = lineList.First(x => x.No_Left == polyLine.No_Left && x.No_Right == polyLine.No_Right && x.PolyLineNo == PolyLineNo.A_始点から中間点);
                lineList.Remove(lineA);
                lineA.X1 = polyLine.X1;
                lineA.Y1 = polyLine.Y1;
                lineA.X2 = polyLine.X2;
                lineA.Y2 = polyLine.Y2;
                lineList.Add(lineA);

                var lineB = lineList.First(x => x.No_Left == polyLine.No_Left && x.No_Right == polyLine.No_Right && x.PolyLineNo == PolyLineNo.B_中間点から中間点);
                lineList.Remove(lineB);
                lineB.X1 = polyLine.X2;
                lineB.Y1 = polyLine.Y2;
                lineB.X2 = polyLine.X3;
                lineB.Y2 = polyLine.Y3;
                lineList.Add(lineB);

                var lineC = lineList.First(x => x.No_Left == polyLine.No_Left && x.No_Right == polyLine.No_Right && x.PolyLineNo == PolyLineNo.C_中間点から終点);
                lineList.Remove(lineC);
                lineC.X1 = polyLine.X3;
                lineC.Y1 = polyLine.Y3;
                lineC.X2 = polyLine.X4;
                lineC.Y2 = polyLine.Y4;
                lineList.Add(lineC);

                //lineList.RemoveAll(x => x.No_Left == polyLine.No_Left && x.No_Right == polyLine.No_Right && x.PolyLineNo == PolyLineNo.A);

                //lineList.Add(new MindRow_Line()
                //{
                //    Type = LineType.PolyLine,
                //    No_Left = polyLine.No_Left,
                //    SuperParentNo_Left = polyLine.SuperParentNo_Left,
                //    No_Right = polyLine.No_Right,
                //    SuperParentNo_Right = polyLine.SuperParentNo_Right,
                //    X1 = polyLine.X1,
                //    Y1 = polyLine.Y1,
                //    X2 = polyLine.X2,
                //    Y2 = polyLine.Y2
                //});

                //lineList.Add(new MindRow_Line()
                //{
                //    Type = LineType.PolyLine,
                //    No_Left = polyLine.No_Left,
                //    SuperParentNo_Left = polyLine.SuperParentNo_Left,
                //    No_Right = polyLine.No_Right,
                //    SuperParentNo_Right = polyLine.SuperParentNo_Right,
                //    X1 = polyLine.X1,
                //    Y1 = polyLine.Y1,
                //    X2 = polyLine.X2,
                //    Y2 = polyLine.Y2
                //});
                //lineList.Add(new MindRow_Line()
                //{
                //    Type = LineType.PolyLine,
                //    No_Left = polyLine.No_Left,
                //    SuperParentNo_Left = polyLine.SuperParentNo_Left,
                //    No_Right = polyLine.No_Right,
                //    SuperParentNo_Right = polyLine.SuperParentNo_Right,
                //    X1 = polyLine.X1,
                //    Y1 = polyLine.Y1,
                //    X2 = polyLine.X2,
                //    Y2 = polyLine.Y2
                //});
            }
        }


        private static void DuplicationY_Approach(ref List<MindRow_Text> textList,
            ref List<MindRow_Line> lineList)
        {
            foreach (var rect in textList)
            {
                var rectListTmp = textList.Where(x => x.KeywordNo == x.SuperParentNo && x.SuperParentNo != rect.SuperParentNo)
                    .Where(x => rect.X1 == x.X1)
                    .Where(x => (rect.Y1 <= x.Y1 && x.Y1 <= rect.Y2) || (rect.Y1 <= x.Y2 && x.Y2 <= rect.Y2));

                if (rectListTmp.Count() < 1)
                {
                    continue;
                }

                foreach (var tmp in rectListTmp)
                {
                    int move;
                    long superParentNo;

                    // Y1が下にある方のItemを、更に下へ移動して重複を解消する。

                    if (!lineList.Any(x => x.SuperParentNo_Left == rect.SuperParentNo) ||
                        !lineList.Any(x => x.SuperParentNo_Left == tmp.SuperParentNo))
                    {
                        continue;
                    }

                    //if (rect.Y1 < tmp.Y1)
                    if (lineList.First(x => x.SuperParentNo_Left == rect.SuperParentNo).Y2 <
                        lineList.First(x => x.SuperParentNo_Left == tmp.SuperParentNo).Y2)
                    {
                        move = rect.Y2 - tmp.Y1;
                        superParentNo = (long)tmp.SuperParentNo;
                    }
                    else
                    {
                        move = tmp.Y2 - rect.Y1;
                        superParentNo = (long)rect.SuperParentNo;
                    }

                    // SuperParent間の余白70を追加。
                    move += 70;

                    foreach (var text in textList.Where(x => x.SuperParentNo == superParentNo))
                    {
                        text.Y1 += move;
                        text.Y2 += move;
                    }

                    foreach (var line in lineList.Where(x => x.SuperParentNo_Left == superParentNo))
                    {
                        line.Y1 += move;

                        //if (tmp.No == line.SuperParentNo_Left)
                        //{
                        //    line.Y1 += move;
                        //}
                        //else if (tmp.No == line.SuperParentNo_Left)
                        //{
                        //    line.Y2 += move;
                        //}
                    }
                    foreach (var line in lineList.Where(x => x.SuperParentNo_Right == superParentNo))
                    {
                        line.Y2 += move;
                    }
                }
            }
        }

        // 親Itemの余白を倍化する為に、ColumnNoの逆順を設定。
        private static void SetInversionColumnNoList(ref List<InversionColumnNo> inversionColumnNoList)
        {
            var inversionColumnNo = 0;
            for (var cnt = inversionColumnNoList.Count - 1; cnt >= 0; cnt--)
            {
                inversionColumnNoList[cnt].InversionNo = inversionColumnNo;

                inversionColumnNo++;
            }
        }


        private static int GetMaxHeight(List<MindRow_Text> textList, int no)
        {
            var childs = textList.Where(x => x.ParentNo == no);

            if (childs.Count() < 1)
            {
                // 子無し。最も下位のItem。

                return 50; // 50は基本の高さ。
            }

            // 子持ち。
            var maxY1 = 0;
            foreach (var child in childs)
            {
                if (maxY1 < child.Y1)
                {
                    // 子が最大Y1を超えてる。
                    maxY1 = child.Y1;
                }
            }

            return maxY1 + 50; // 50は基本の高さ。
        }

        //private static void PositionCalculation_Rect_Step1(List<MindRow_Text> textList,
        //    ref List<MindRow_Rect> rectList)
        //{
        //    foreach (var item in textList)
        //    {
        //        //var width = item.Width;
        //        //int x1 = item.X1;
        //        //int y1 = item.Y1;
        //        //var inversionColumnNoList = new List<InversionColumnNo>();

        //        //GetMaxWidth(textList, item, ref x1, ref y1, ref width, ref inversionColumnNoList);

        //        //SetInversionColumnNoList(ref inversionColumnNoList);
        //        //var inversionColumnNo = inversionColumnNoList.First(x => x.ColumnNo == item.ColumnNo).InversionNo;

        //        var rect = rectList.First(x => x.No == item.No);
        //        rect.X1 = item.X1 - 10;
        //        rect.Y1 = item.Y1 - 25;
        //        //rect.Width = ((x1 - item.X1) * inversionColumnNo) + width + 20;
        //        rect.Width = item.Width + 20;
        //        //rect.Height = y1 - item.Y1 + 50 + (20 * inversionColumnNo);
        //        rect.Height = 40;
        //    }
        //}


        //private static MindRow UnikktileConvertToClass_Row_Rect(string[] unikktileRow)
        //{
        //    var mindRow = new MindRow();

        //    mindRow.Type = MindType.Rect;
        //    mindRow.OrderNo = short.Parse(unikktileRow[1]);
        //    mindRow.X1 = int.Parse(unikktileRow[2]);
        //    mindRow.Y1 = int.Parse(unikktileRow[3]);
        //    mindRow.Width = int.Parse(unikktileRow[4]);
        //    mindRow.Height = int.Parse(unikktileRow[5]);
        //    mindRow.Fill = unikktileRow[6];
        //    mindRow.Stroke = unikktileRow[7];
        //    mindRow.StrokeWidth = byte.Parse(unikktileRow[8]);

        //    return mindRow;
        //}

        //private static MindRow UnikktileConvertToClass_Row_Line(string[] unikktileRow)
        //{
        //    var mindRow = new MindRow();

        //    mindRow.Type = MindType.Line;
        //    mindRow.OrderNo = short.Parse(unikktileRow[1]);
        //    mindRow.X1 = int.Parse(unikktileRow[2]);
        //    mindRow.Y1 = int.Parse(unikktileRow[3]);
        //    mindRow.X2 = int.Parse(unikktileRow[4]);
        //    mindRow.Y2 = int.Parse(unikktileRow[5]);
        //    mindRow.Stroke = unikktileRow[6];
        //    mindRow.StrokeWidth = byte.Parse(unikktileRow[7]);

        //    return mindRow;
        //}

        //private static MindRow UnikktileConvertToClass_Row_Text(string[] unikktileRow)
        //{
        //    var mindRow = new MindRow();

        //    mindRow.Type = MindType.Text;
        //    mindRow.OrderNo = short.Parse(unikktileRow[1]);
        //    mindRow.X1 = int.Parse(unikktileRow[2]);
        //    mindRow.Y1 = int.Parse(unikktileRow[3]);
        //    mindRow.FontFamily = unikktileRow[4];
        //    mindRow.FontSize = byte.Parse(unikktileRow[5]);
        //    mindRow.Fill = unikktileRow[6];
        //    mindRow.Sentence = unikktileRow[7];

        //    return mindRow;
        //}

        //private static MindRow UnikktileConvertToClass_Row_Link(string[] unikktileRow)
        //{
        //    var mindRow = new MindRow();

        //    mindRow.Type = MindType.Link;
        //    mindRow.OrderNo = short.Parse(unikktileRow[1]);
        //    mindRow.X1 = int.Parse(unikktileRow[2]);
        //    mindRow.Y1 = int.Parse(unikktileRow[3]);
        //    mindRow.FontFamily = unikktileRow[4];
        //    mindRow.FontSize = byte.Parse(unikktileRow[5]);
        //    mindRow.Fill = unikktileRow[6];
        //    mindRow.Sentence = unikktileRow[7];
        //    mindRow.URL = unikktileRow[8];

        //    return mindRow;
        //}


        private static void X1Y1が重複するアイテムはY軸の重複を解消する_子アイテム再帰(
            int y差, MindRow_Text duplicationText, ref List<MindRow_Text> textList)
        {
            var 子Items = textList.Where(x => x.ParentNo == duplicationText.KeywordNo);

            if (子Items.Count() < 1)
            {
                return;
            }

            foreach (var 子Item in 子Items)
            {
                子Item.Y1 += y差;
                子Item.Y2 += y差;

                X1Y1が重複するアイテムはY軸の重複を解消する_子アイテム再帰(y差, 子Item, ref textList);
            }
        }


    }
}
