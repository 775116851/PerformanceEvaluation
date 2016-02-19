function getMenuListByLevel(sourceList, level) {
    if (isNaN(level) || !sourceList) return;
    var result = [];
    for (var i = 0; i < sourceList.length; i++) {
        var temp = sourceList[i];
        if (temp.Level == level) {
            result.push(temp);
        }
    }
    result.sort(function (a, b) { return a.SortNo - b.SortNo; });
    return result;
}