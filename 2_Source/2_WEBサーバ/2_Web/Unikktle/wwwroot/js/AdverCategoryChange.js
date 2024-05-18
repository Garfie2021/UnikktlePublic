// 関数名はjs全体でユニークじゃないとダメ。

function ChangeAdverCategory(obj) {
    // console.log("ChangeAdverCategory()");

    var idx = obj.selectedIndex;
    var value = obj.options[idx].value; // 値

    if (value === 1) {
        // 無料
        $("#abd").fadeOut();
    }
    else {
        // 有料
        $("#abd").fadeIn();
    }
}
