// 関数名はjs全体でユニークじゃないとダメ。
// site.min.js に纏めれないjsは「Page_」付き。

$('#SearchText').keypress(function (e) {
    if (e.which === 13) {
        //console.log("Enter()");
        ClickSearch();
    }
});

function ClickSearch() {
    //console.log("ClickSearch()");

    var searchString = $('#SearchText').val();
    if (searchString === "") {
        return false;
    }
    else {
        sessionStorage.setItem("SearchString", searchString);

        var pageNum = 0;
        sessionStorage.setItem("pageNum", pageNum);

        $("#s").remove();
        $("#w").append('<div id="s"></div>');

        S(searchString, pageNum);
    }
}

function ClickNext() {
    //console.log("ClickNext()");

    var pageNum = sessionStorage.getItem("pageNum");
    var searchString = sessionStorage.getItem("SearchString");

    S(searchString, pageNum);
}


function S(searchString, pageNum) {
    //console.log('pageNum =' + pageNum);

    $.getJSON("/SearchAdver?s=" + encodeURIComponent(searchString) + "&p=" + pageNum).then(
        function (data) {

            const wordList = data["l"];   // "keywordList"

            if (wordList.length < 1 && pageNum < 1) {
                $("#s").append('<p>0 hits. Not found...</p>');
            }
            else {
                var table = $('<table class="table Fx5 Nowrap">');
                var tbody = $("<tbody>");

                for (var i = 0; i < wordList.length; i++) {
                    // wordList[i].i    // id
                    // wordList[i].w    // word
                    tbody.append('<tr><td><a href="/Identity/Account/Manage/AdverRelationWordEdit?r=' + wordList[i].i + '">' + wordList[i].w + '</a></td></tr>');
                    table.append(tbody);
                }

                $("#s").append(table);

                // ViewNextボタン再作成
                $("#ViewNext").remove();

                const NextAvailable = data["n"];    // "nextAvailable"

                if (NextAvailable === true) {
                    $("#s").append('<input id="ViewNext" class="btn btn-primary sample4" onclick="ClickNext()" value="View next 30." />');
                }
            }
        }
    );

    sessionStorage.setItem("pageNum", Number(pageNum) + 1);
}
