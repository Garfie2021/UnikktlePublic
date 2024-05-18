// 関数名はjs全体でユニークじゃないとダメ。
// site.min.js に纏めれないjsは「Page_」付き。

//// localStorageは使わない。
//// sessionStorageはセッション継続中のみ保持される。

function init(SvgW, SvgH, ModelId) {
    // console.log("init()");

    sessionStorage.setItem("svg_width", SvgW);
    sessionStorage.setItem("svg_height", SvgH);

    sessionStorage.setItem("PageNum-" + ModelId + "-0", 1); // 最初の列なので列Noは0。

    // 1つ前のIdを除外対象として登録
    sessionStorage.setItem("vNo", 1);
    sessionStorage.setItem("excludeId_1", ModelId);
}

function ClickExternalSearch(url) {
    const RectSelectAll = $(".RectSelect");
    var urlpara = "";
    for (var i = 0; i < RectSelectAll.length; i++) {
        urlpara += encodeURIComponent($("#text-" + RectSelectAll[i].id).text()) + "+";
    }
    urlpara = urlpara.slice(0, -1);

    window.open(url + urlpara, '_blank');
}

function RectMouseenter(id) {
    // マウスが乗ったとき
    //console.log("text hover in =>" + id);
    //var id = $(this).attr("id");
    //if (id.indexOf("Border") > -1) return;
    $("#" + id).css({ fill: RectFill, stroke: RectStroke });
}

function RectMouseleave(id) {
    // マウスが離れたとき（elseなどは使わない）
    //var id = $(this).attr("id");
    //console.log("rect hover out =>" + id);
    //if (id.indexOf("Border") > -1) return;

    var rectClass = $("#" + id).attr("class");
    //console.log(rectClass);

    if (rectClass === "RectSelect") {
        $("#" + id).css({ fill: RectFill_Mouseleave });
    } else {
        $("#" + id).css({ fill: RectFill_Mouseleave, stroke: RectStroke_Mouseleave });
    }
}

function TextMouseenter(id) {
    // マウスが乗ったとき
    //var id = $(this).attr("id");
    var id2 = id.substring(id.indexOf("-") + 1);
    //console.log("text hover in =>" + id);
    $("#" + id2).css({ fill: RectFill, stroke: RectStroke });
}

function TextMouseleave(id) {
    // マウスが離れたとき（elseなどは使わない）
    //var id = $(this).attr("id");
    var id2 = id.substring(id.indexOf("-") + 1);
    var rectClass = $("#" + id2).attr("class");
    //console.log(rectClass);

    if (rectClass === "RectSelect") {
        $("#" + id2).css({ fill: RectFill_Mouseleave });
    } else {
        $("#" + id2).css({ fill: RectFill_Mouseleave, stroke: RectStroke_Mouseleave });
    }
}

function NextTextMouseenter(id) {
    // マウスが乗ったとき
    //var id = $(this).attr("id");
    var id2 = id.substring(id.indexOf("-") + 1);
    //console.log("text hover in =>" + id);
    //$("#" + id2).css({fill: "#0069D9" });
    NextRectMouseenter(id2);
}

function NextTextMouseleave(id) {
    // マウスが離れたとき（elseなどは使わない）
    //var id = $(this).attr("id");
    var id2 = id.substring(id.indexOf("-") + 1);
    //console.log(rectClass);

    //$("#" + id2).css({fill: "#007BFF" });
    NextRectMouseleave(id2);
}

// https://localhost:44376/Home/WordMap/176635 を表示し、「No:176635 Word:AA」キーワードMapの値。
// id="NextRectGrID-176635-0"
// NextRectMouseenter(this.id) <= this.id="NextRectGrID-176635-0"
function NextRectMouseenter(id) {
    $("#" + id).css({ fill: "#0069D9" });
}

// https://localhost:44376/Home/WordMap/176635 を表示し、「No:176635 Word:AA」キーワードMapの値。
// id="NextRectGrID-176635-0"
// NextRectMouseleave(this.id) <= this.id="NextRectGrID-176635-0"
function NextRectMouseleave(id) {
    $("#" + id).css({ fill: "#007BFF" });
}

function ClickText(id, ModelId) {
    // console.log("ClickText() id=" + id + "  ModelId=" + ModelId);
    //console.log("id.indexof()=" + id.indexOf("-"));
    //var idKey = id.substring(id.indexOf("-"));
    //console.log("idKey=" + idKey);

    ClickRect(id.substring(id.indexOf("-") + 1), ModelId);
}

// https://localhost:44376/Home/WordMap/176635 を表示し、「No:176635 Word:AA」キーワードMapの値。
// 「AA」の関連キーワード「% AA」の値。
// id="1062137-1" <= 「No:1062137 Word:% AA」
// ClickRect(this.id, '176635')"  <= this.id="NextRectGrID-176635-0"
function ClickRect(id, ModelId) {
    // console.log("ClickRect() id=" + id + "  ModelId=" + ModelId);

    var jqSelect = $("#" + id);
    var jqSelectSts = jqSelect.attr('class');
    var parentGrID = jqSelect.parent().attr("id");
    var GrID = "GrID-" + id;

    if (jqSelectSts === 'RectSelect') {
        jqSelect.attr('class', 'RectNonSelect');

        console.log("ClickRect-1");

        N(GrID);
    }
    else {
        jqSelect.attr('class', 'RectSelect');

        console.log("ClickRect-2");

        var idSplit = id.split('-');
        var No = idSplit[0];
        var vNo = idSplit[1];

        F(parentGrID, GrID);

        var PageNum_ModelId_vNo = "PageNum-" + No + "-" + vNo;

        console.log("ClickRect-3");

        Q(GrID, id, 0, No, vNo, ModelId, PageNum_ModelId_vNo);
    }
}


function ClickNextText(id, ModelId) {
    // console.log("ClickNextText() id=" + id + "  ModelId=" + ModelId);
    //console.log("ClickText() id=" + id);
    //console.log("id.indexof()=" + id.indexOf("-"));
    //var idKey = id.substring(id.indexOf("-"));
    //console.log("idKey=" + idKey);

    ClickNext(id.substring(id.indexOf("-") + 1), ModelId);
}

// https://localhost:44376/Home/WordMap/176635
// 「No: 176635 Word: AA」キーワードの関連キーワードMapを表示した場合。
// 　id="NextRectGrID-176635-0"
// 　ClickNext(this.id, '176635') <= this.id="NextRectGrID-176635-0"
function ClickNext(id, ModelId) {
    // console.log("ClickNext() id=" + id + "  ModelId=" + ModelId);

    var idSplit = id.split('-');
    var No = idSplit[1];
    var vNo = idSplit[2]; // 列No

    var jqSelect = $("#" + id);
    var GrID = jqSelect.parent().attr("id");

    var PageNum_ModelId_vNo = "PageNum-" + ModelId + "-" + vNo;

    // console.log("PageNum_ModelId_vNo=" + PageNum_ModelId_vNo);
    var pageNum = Number(sessionStorage.getItem(PageNum_ModelId_vNo));
    // console.log("pageNum=" + pageNum);

    // console.log(GrID, id, pageNum, No, vNo);
    Q(GrID, id, pageNum, No, vNo, ModelId, PageNum_ModelId_vNo);
}

function Q(GrID, id, pageNum, No, vNo, ModelId, PageNum_ModelId_vNo) {
    // console.log("Q()");

    var url = "/MapPR/" + No + "?p=" + pageNum;

    console.log("url=" + url);

    d3.json(url).then(
        function (adverSelectPrRelation) {
            // console.log("adverSelectPrRelation=" + adverSelectPrRelation);
            M(GrID, id, pageNum, No, vNo, ModelId, PageNum_ModelId_vNo, adverSelectPrRelation);
        }
    );
}

// d3json_Map
function M(GrID, id, pageNum, No, vNo, ModelId, PageNum_ModelId_vNo, adverSelectPrRelation) {
    console.log("M()");
    //console.log("M()   GrID=" + GrID + " id=" + id);

    var GrIDpage = GrID + "-" + pageNum;
    console.log("GrIDpage=" + GrIDpage);

    var data = sessionStorage.getItem(GrIDpage);

    //console.log(data);

    if (data === null) {
        // セッションデータが無い

        console.log("M() 1");

        //console.log("excludeId_" + idSplit[0]);
        var ExcludeId = sessionStorage.getItem("excludeId_" + vNo);
        //console.log("M() getItem() excludeId_" + vNo + " = " + ExcludeId);

        // /Map/{id}?ExcludeId={ExcludeId}
        var url = "/Map/" + No + "?p=" + pageNum;

        if (ExcludeId !== null) {
            url += "&e=" + ExcludeId;
        }

        console.log("url=" + url);
        d3.json(url).then(
            function (data) {
                sessionStorage.setItem(GrIDpage, JSON.stringify(data));
                E(data, id, GrID, pageNum, No, ModelId, adverSelectPrRelation);
            }
        );
    } else {
        console.log("M() 2");

        E(JSON.parse(data), id, GrID, pageNum, No, ModelId, adverSelectPrRelation);
    }

    sessionStorage.setItem(PageNum_ModelId_vNo, pageNum + 1);
}

// d3json_Map_Exec
function E(data, id, GrID, pageNum, No, ModelId, adverSelectPrRelation) {
    console.log("E()");
    console.log(data);
    //console.log("E() id=" + id + " GrID=" + GrID);
    // console.log(data);

    //const adverSelectPrRelation = data.adverSelectPrRelation;
    // console.log(adverSelectPrRelation);

    console.log(data.wordList);
    const wordList = data.wordList;
    
    //const NextAvailable = data.nextAvailable;
    const NextAvailable = data.nextAvailable;

    //console.log("NextAvailable=" + NextAvailable);

    const svg = d3.select("#s");
    var group;

    if (pageNum === 0) {
        group = svg.append("g");
        group.attr("id", GrID);
    } else {
        group = d3.select("#" + GrID);
    }

    const select = $("#" + id);
    const select_x = Number(select.attr("x"));
    const select_y = Number(select.attr("y"));
    const select_width = Number(select.attr("width"));
    //const select_height = Number(select.attr("height"));
    const line_y = select_y + 15;
    const line_x_stt = select_x + select_width;
    const line_x_end = select_x + select_width + 30;
    var rect_x;

    if (pageNum === 0) {
        rect_x = line_x_end + 20;
    } else {
        rect_x = select_x;
    }

    const margin_y = 30;
    const text_x = rect_x + 10;
    const rect_y = select_y + margin_y;
    const text_y = select_y + 50;

    var vNo = Number(sessionStorage.getItem("vNo"));
    if (pageNum === 0) {
        vNo += 1;
        sessionStorage.setItem("vNo", vNo);
    }

    // 1つ前のIdを除外対象として登録
    //console.log("E() setItem() excludeId_" + vNo, No);
    sessionStorage.setItem("excludeId_" + vNo, No);

    S(pageNum, group, line_x_stt, line_y, line_x_end);

    var rectBorder_width = 0;
    var rectBorder_height = 0;
    console.log("E() 1");

    if (wordList.length > 0) {
        console.log("E() 2");

        for (var i1 = 0; i1 < wordList.length; i1++) {
            if (rectBorder_width < wordList[i1].r_w) {
                rectBorder_width = wordList[i1].r_w;
            }
        }

        rectBorder_width += 40;
        rectBorder_height = (wordList.length) * 40;

        if (pageNum === 0) {
            rectBorder_height += 80;
        } else {
            rectBorder_height += margin_y;
        }
    } else {
        console.log("E() 3");

        rectBorder_width = 200;
        rectBorder_height = 70;
    }
    
    // Idが1なら広告が有る
    if (adverSelectPrRelation.i > 0) {
        //console.log("AdverTitle_r_w: " + adverSelectPrRelation.adverTitle_r_w);
        //console.log("rectBorder_width: " + rectBorder_width);

        if (adverSelectPrRelation.adverTitle_r_w > rectBorder_width) {
            rectBorder_width = adverSelectPrRelation.adverTitle_r_ww;
        }

        rectBorder_height = rectBorder_height + 40;
    }

    R(pageNum, group, GrID, line_x_end, select_y, rectBorder_width, rectBorder_height);

    G(svg, line_x_end, rectBorder_width, select_y, rectBorder_height);

    var pr_y = P(group, adverSelectPrRelation, rect_x, rect_y, data);

    I(wordList, pageNum, group, rect_x, rect_y, vNo, pr_y, text_x, text_y, No);

    X(pageNum, NextAvailable, group, GrID, rect_x, rect_y, wordList.length, pr_y, text_x, text_y, No);
}

// SetLine
function S(pageNum, group, line_x_stt, line_y, line_x_end) {
    console.log("S()");

    if (pageNum === 0) {
        group.append("line")
            .attr("x1", line_x_stt)
            .attr("y1", line_y)
            .attr("x2", line_x_end)
            .attr("y2", line_y)
            .attr("class", "RectBorder");
    }
}

function R(pageNum, group, GrID, line_x_end, select_y, rectBorder_width, rectBorder_height) {
    console.log("R()");

    if (pageNum === 0) {
        group.append("rect")
            .attr("id", "Border" + GrID)
            .attr("x", line_x_end)
            .attr("y", select_y)
            .attr("width", rectBorder_width)
            .attr("height", rectBorder_height)
            .attr("class", "RectBorder");
    } else {
        //console.log("#" + "Border" + GrID);
        const selectBorder = $("#" + "Border" + GrID);
        //console.log(selectBorder);
        //console.log(rectBorder_width);
        var rectBorder_width_old = Number(selectBorder.attr("width"));
        //selectBorder.attr("width", Number(rectBorder_width_old) + rectBorder_width);
        if (rectBorder_width_old < rectBorder_width) {
            selectBorder.attr("width", rectBorder_width);
        }

        const rectBorder_height_old = Number(selectBorder.attr("height"));
        selectBorder.attr("height", rectBorder_height_old + rectBorder_height);
    }
}

// SetSVG
function G(svg, line_x_end, rectBorder_width, select_y, rectBorder_height) {
    console.log("G()");

    const svg_width = sessionStorage.getItem("svg_width");
    const svg_height = sessionStorage.getItem("svg_height");
    const svg_width_new = line_x_end + rectBorder_width + 100;
    const svg_height_new = select_y + rectBorder_height + 100;

    if (svg_width < svg_width_new) {
        svg.attr("width", Number(svg_width_new));
        sessionStorage.setItem("svg_width", svg_width_new);
    }

    if (svg_height < svg_height_new) {
        svg.attr("height", Number(svg_height_new));
        sessionStorage.setItem("svg_height", svg_height_new);
    }
}

// PRAdd
function P(group, adverSelectPrRelation, rect_x, rect_y, data) {
    console.log("P()");

    if (adverSelectPrRelation.i > 0) {
        var pr = group.append("text")
            //.attr("id", prCountText)
            .attr("id", "pr-1")
            .attr("x", rect_x)
            .attr("y", rect_y)
            .attr("class", "PrText");

        var prAnchor = pr.append("a")
            .attr("href", "#")
            // au：adverURL
            // u：userNo
            // b：businessNo
            // a：adverNo
            // w：wordId
            .attr("onclick", "ClickPrRelation('" + adverSelectPrRelation.au + "', " + adverSelectPrRelation.u + ", " + adverSelectPrRelation.b + ", " + adverSelectPrRelation.a + ", " + data.wordId + ");return false;");

        prAnchor.append("tspan")
            .attr("x", rect_x)
            .attr("y", rect_y)
            .text("[PR] " + adverSelectPrRelation.adverTitle1);

        prAnchor.append("tspan")
            .attr("x", rect_x)
            .attr("y", rect_y + 20)
            .text(adverSelectPrRelation.adverTitle2);

        return 40;
    }

    return 0;
}

// SetRectTextItems
function I(wordList, pageNum, group, rect_x, rect_y, vNo, pr_y, text_x, text_y, No) {
    console.log("I()");

    if (wordList.length < 1) {
        console.log("I() 1");

        if (pageNum === 0) {
            // 2列目以降で、最初の30件が0件だった場合。
            group.append("text")
                .attr("x", rect_x)  // text_x ではなく rect_x で正しい
                .attr("y", rect_y)  // text_y ではなく rect_y で正しい
                .attr("class", "HitsText")
                .text("0 hits. Not calculated...");
        }
    } else {
        console.log("I() 2");

        var i2 = 0;
        for (i2 = 0; i2 < wordList.length; i2++) {
            console.log("I() 2-1");

            //console.log("rect_x = " + rect_x);
            //console.log("rect_y = " + rect_y);
            //console.log("wordList[i2].r_w = " + wordList[i2].r_w);

            group.append("rect")
                .attr("id", wordList[i2].id + "-" + vNo)
                .attr("onmouseenter", "RectMouseenter(this.id)")
                .attr("onmouseleave", "RectMouseleave(this.id)")
                .attr("onclick", "ClickRect(this.id, '" + No + "')")
                .attr("x", rect_x)
                .attr("y", rect_y + i2 * 40 + pr_y)
                .attr("width", wordList[i2].r_w)
                .attr("height", 30)
                .attr("class", "RectNonSelect");

            console.log("I() 2-2");
            console.log("wordList[i2].id = " + wordList[i2].id);
            console.log("vNo = " + vNo);
            console.log("No = " + No);
            console.log("text_x = " + text_x);
            console.log("text_y = " + text_y);
            console.log("pr_y = " + pr_y);
            console.log("wordList[i2].word = " + wordList[i2].word);

            group.append("text")
                .attr("id", "text-" + wordList[i2].id + "-" + vNo)
                .attr("onmouseenter", "TextMouseenter(this.id)")
                .attr("onmouseleave", "TextMouseleave(this.id)")
                .attr("onclick", "ClickText(this.id, '" + No + "')")
                .attr("x", text_x)
                .attr("y", text_y + i2 * 40 + pr_y)
                .attr("class", "RectText")
                .text(wordList[i2].word);

            console.log("I() 2-3");
        }
    }
}

// SetNextRect
function X(pageNum, NextAvailable, group, GrID, rect_x, rect_y, i, pr_y, text_x, text_y, No) {
    console.log("X()");

    if (pageNum === 0) {
        //console.log("X() 1");

        if (NextAvailable) {
            //console.log("X() 1-1");
            //console.log("GrID :" + GrID);
            //console.log("No :" + No);
            //console.log("rect_x :" + rect_x);
            //console.log("rect_y :" + rect_y);
            //console.log("pr_y :" + pr_y);
            //console.log("text_x :" + text_x);
            //console.log("text_y :" + text_y);

            group.append("rect")
                .attr("id", "NextRect" + GrID)
                .attr("onmouseenter", "NextRectMouseenter(this.id)")
                .attr("onmouseleave", "NextRectMouseleave(this.id)")
                .attr("onclick", "ClickNext(this.id, '" + No + "')")
                .attr("x", rect_x)
                .attr("y", rect_y + i * 40 + pr_y)
                .attr("width", 150)
                .attr("height", 30)
                .attr("class", "RectNext");

            group.append("text")
                .attr("id", "text-NextRect" + GrID)
                .attr("onmouseenter", "NextTextMouseenter(this.id)")
                .attr("onmouseleave", "NextTextMouseleave(this.id)")
                .attr("onclick", "ClickNextText(this.id, '" + No + "')")
                .attr("x", text_x)
                .attr("y", text_y + i * 40 + pr_y)
                .attr("class", "RectTextNext")
                .text("View next 30");
        }

        //console.log("X() 1-2");

    } else {
        //console.log("X() 2");

        //console.log("#NextRect" + GrID);
        // Nextボタンを一番下に移動
        const NextRect = $("#NextRect" + GrID);
        const NextText = $("#text-NextRect" + GrID);

        if (NextAvailable) {
            NextRect.attr("x", rect_x);
            NextRect.attr("y", rect_y + i * 40 + pr_y);
            NextText.attr("x", text_x);
            NextText.attr("y", text_y + i * 40 + pr_y);
        }
        else {
            NextRect.remove();
            NextText.remove();
        }
    }
}




// fRectNonSelect
function N(GrID) {
    console.log("N()");

    var sessionGrIdArrayList = JSON.parse(sessionStorage.getItem("GrIdArrayList"));
    C(sessionGrIdArrayList, GrID);

    for (var i1 = 0; i1 < sessionGrIdArrayList.length; i1++) {
        if (sessionGrIdArrayList[i1][0] === GrID) {
            sessionGrIdArrayList.splice(i1, 1);
        }
    }

    for (var i2 = 0; i2 < sessionGrIdArrayList.length; i2++) {
        if (sessionGrIdArrayList[i2][0] === GrID) {
            sessionGrIdArrayList.splice(i2, 1);
        }
    }

    for (var i3 = 0; i3 < sessionGrIdArrayList.length; i3++) {
        if (sessionGrIdArrayList[i3][1] === GrID) {
            sessionGrIdArrayList.splice(i3, 1);
        }
    }

    sessionStorage.setItem("GrIdArrayList", JSON.stringify(sessionGrIdArrayList));
}

// RectRemove
function C(sessionGrIdArrayList, GrId) {
    console.log("C()");

    for (var i = 0; i < sessionGrIdArrayList.length; i++) {
        if (sessionGrIdArrayList[i][0] === GrId) {
            C(sessionGrIdArrayList, sessionGrIdArrayList[i][1]);
        }
    }

    $("#" + GrId).remove();
}

// fRectRemoveFollow
function F(parentGrId, GrID) {
    console.log("F()");

    var GrIdArray = [parentGrId, GrID];

    var sessionGrIdArrayList = JSON.parse(sessionStorage.getItem("GrIdArrayList"));
    if (sessionGrIdArrayList !== null) {
        sessionGrIdArrayList.push(GrIdArray);
    } else {
        sessionGrIdArrayList = [GrIdArray];
    }

    sessionStorage.setItem("GrIdArrayList", JSON.stringify(sessionGrIdArrayList));
}

