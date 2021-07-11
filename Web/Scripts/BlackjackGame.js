///<reference path="./jquery-1.6.1-vsdoc.js" />

(function ($) {

    var banner,
        message,
        score,
        isGameExists = false,
        userScore,
        computerScore,
        winsCount,
        drawsCount,
        loosesCount,
        scoreHistory;

    /*
    Helpers
    */
    function updateWindow(data) {
        userScore.html(data.PlayerScore);
        updateContainer($('#playerContainer .hand'), data.PlayerHand, false);

        computerScore.html(data.DealerScore);
        updateContainer($('#dealerContainer .hand'), data.DealerHand, data.ShowDealerBlankCard);

        winsCount.html(data.WinsCounter);
        drawsCount.html(data.DrawsCounter);
        loosesCount.html(data.LoosesCounter);

        if (null != data.StatisticTable) {
            isGameExists = false;
            updateStatistic(data.StatisticTable);
        }
        else {
            isGameExists = true;
        }
    };

    function updateStatistic(data) {
        var table = ['<tr>'];
        for (var index = 0, length = data.length; index < length; index++) {
            table.push('<td>' + data[index] + '</td>');
        }
        table.push('</tr>');
        var html = table.join('');
        $('.scoreHistory table tbody').append(html);
        showScores();
    }

    function updateContainer(container, data, showBlank) {
        var html = data.join('');
        if (showBlank) {
            html += "<div class='card blank'></div>";
        }

        container.html(html);
    };

    function post(element) {
        var url = element.attr('data-url');
        $.post(url, updateWindow);
    }

    function showScores() {
        banner.show();
        message.hide();
        score.show();
        scoreHistory.scrollTop(scoreHistory.children().first().height());
    };

    /*
    Handlers
    */

    function showScoresClick(e) {
        e.preventDefault();
        
        showScores();
        isGameExists = false;

        return false;
    };

    function playButtonClick(e) {

        e.preventDefault();

        banner.hide();
        isGameExists = true;

        return false;
    };

    function resetGameClick(e) {
        e.preventDefault();

        var element = $(e.target);
        post(element);
        banner.hide();
        isGameExists = true;
        return false;
    };

    function actionClick(e) {
        if (isGameExists) {
            var element = $(e.target);
            post(element);
        }
    };

    $(function () {
        //cache html elements
        winsCount = $('#stats p:nth-child(1) span');
        drawsCount = $('#stats p:nth-child(2) span');
        loosesCount = $('#stats p:nth-child(3) span');
        userScore = $('#user-score');
        computerScore = $('#computer-score');
        banner = $('#messageBackground');
        message = $('#message');
        score = $('#score-container');
        scoreHistory = $('.scoreHistory');

        //bind handlers
        $('.close-banner').click(playButtonClick);
        $('#twist, #stick').click(actionClick);
        $('.reset-game').click(resetGameClick);
        $('.show-scores').click(showScoresClick);

        //set state
        if (!banner.is(':visible')) {
            isGameExists = true;
        }
    });
})($);