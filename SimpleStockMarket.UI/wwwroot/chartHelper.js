window.StockChart = {
    createChart: function (canvasId, stockData) {
        var ctx = document.getElementById(canvasId).getContext('2d');
        var stockChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: stockData.labels,
                datasets: [{
                    label: stockData.symbol + ' Stock Prices',
                    data: stockData.data,
                    borderColor: 'blue',
                    fill: false
                }]
            }
        });
    }
};
