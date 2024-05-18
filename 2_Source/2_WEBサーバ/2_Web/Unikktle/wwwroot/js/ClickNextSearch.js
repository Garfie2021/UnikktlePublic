// 関数名はjs全体でユニークじゃないとダメ。

function ClickNextSearch() {
    // console.log("ClickNext()");

    var SearchString = sessionStorage.getItem("SearchString");
    var pageNum = sessionStorage.getItem("pageNum");
    // console.log('pageNum =' + pageNum);

    //var test = "/Search?SearchString=" + encodeURIComponent(SearchString) + "&pageNum=" + pageNum;
    //console.log("/Search?SearchString=c%23&pageNum=0");
    //console.log(test);

    //$.getJSON("/Search?SearchString=" + encodeURIComponent(SearchString) + "&pageNum=" + pageNum).then(
    $.getJSON("/Search?s=" + encodeURIComponent(SearchString) + "&p=" + pageNum).then(
        function (data) {
            //console.log(data);

            const wordList = data["l"];       // "keywordList"
            const adverSelectPR = data["a"];  // "adverSelectPrSearch"

            if (adverSelectPR === null) {
                $("#s").append('<div>[PR]</div><p></p>');
            } else {
				// href="#"を削除するとリンクで表示されない。必要。
                var pr = '<a href="#" onclick="ClickPrSearch(' + "'" +
                    adverSelectPR.au +  // adverURL
                    "'" + ',' +
                    adverSelectPR.u +   // userNo
                    ',' +
                    adverSelectPR.b +   // businessNo
                    ',' +
                    adverSelectPR.a +   // adverNo
                    ');return false;">[PR] ' +
                    adverSelectPR.t1 +  // adverTitle1
                    '<br>' +
                    adverSelectPR.t2 +  // adverTitle2
                    '</a>';

                $("#s").append('<div>' + pr + '</div><p></p>');
            }

            var table = $('<table class="table Fx5 Nowrap">');
            var tbody = $("<tbody>");

            for (var i = 0; i < wordList.length; i++) {
                // wordList[i].i    // id
                // wordList[i].w    // word
                tbody.append('<tr><td><a href="/Home/WordMap/' + wordList[i].i + '?p=0">' + wordList[i].w + '</a></td></tr>');
                table.append(tbody);
            }

            $("#s").append(table);

            const NextAvailable = data["n"];    // "nextAvailable"

            if (NextAvailable === false) {
                $("#ViewNext").remove();
            }
        }
    );

    sessionStorage.setItem("pageNum", Number(pageNum) + 1);
}
