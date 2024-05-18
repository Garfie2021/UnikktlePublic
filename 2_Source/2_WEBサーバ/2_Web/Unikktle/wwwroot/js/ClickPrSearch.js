// 関数名はjs全体でユニークじゃないとダメ。

function ClickPrSearch(url, uid, bid, aid, wid) {
    // console.log("ClickPrSearch()");
    //console.log("url = " + url);

    // クリック履歴
    var urlClick = "/ClickPrSearch?u=" + uid + "&b=" + bid + "&a=" + aid + "&w=" + wid;
    $.ajax({ url: urlClick });

    // 広告URLを別ページで表示
    window.open(url, '_blank');
}
