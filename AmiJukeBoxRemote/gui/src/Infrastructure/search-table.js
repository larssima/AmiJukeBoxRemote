export class SearchTable {
    constructor() {
        this.searchInput = '';
        this.table = '';
        this.tableTrs = '';
    }

    initSearchTable(searchInputId, tableId) {
        var _this = this;

        this.searchInput = '#' + searchInputId;
        this.table = '#' + tableId;
        this.tableTrs = this.table + ' tr';

        if (this.validateInputs() && $(this.tableTrs).length) {
            $(this.searchInput).on("keyup", function() {
                var value = $(this).val();

                _this.removeHighlighting($("table tr"));

                //Each row
                $("table tr").each(function(rowIndex) {
                    if (rowIndex !== 0) {
                        var $row = $(this);

                        var $tdElement = $row.find("td");

                        var cellIsMatched = false;

                        //Each cell
                        $($tdElement).each(function(cellIndex) {
                            var $cell = $(this);

                            var id = $cell.text();
                            var matchedIndex = id.indexOf(value);

                            if (matchedIndex == -1 && cellIsMatched == false) {
                                //Not matched, hide
                                $row.hide();
                            } else {
                                if (value.length > 0) {
                                    if (matchedIndex != -1) {
                                        //Matched, show and hightlight
                                        _this.addHighlighting($cell);

                                        $row.show();

                                        cellIsMatched = true;
                                    } else {
                                        _this.removeHighlighting($cell);
                                    }
                                } else {
                                    //Search empty
                                    $row.show();

                                    $('.search-table-match').each(function() {
                                        $(this).removeClass('search-table-match');
                                    });
                                }
                            }
                        });
                    }
                });
            });
        } else {
            alert('Search table: Element(s) not found.');
        }
    }

    removeHighlighting(trs) {
        trs.each(function() {
            $(this).removeClass('search-table-match');
        });
    }

    addHighlighting(cell) {
        $(cell).addClass('search-table-match');
    }

    //strReplaceAll(string, Find, Replace) {
    //    try {
    //        return string.replace( new RegExp(Find, "g"), Replace ); //g == case sensetive, gi == case insensitive
    //    } catch(ex) {
    //        return string;
    //    }
    //}

    validateInputs() {
        var searchInput = $(this.searchInput).length;
        var table = $(this.table).length;

        if (searchInput && table) {
            return true;
        } else {
            return false;
        }
    }
}