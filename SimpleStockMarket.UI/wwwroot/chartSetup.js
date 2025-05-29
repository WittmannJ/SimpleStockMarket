function renderStockChart(chartId, labels, prices, symbol) {
    var ctx = document.getElementById(chartId).getContext('2d');
    new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: symbol + " Stock Price",
                data: prices,
                borderColor: 'rgb(75, 192, 192)',
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                fill: true,
                pointRadius: 0 // Removes the small circles on each data point
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false
        }
    });
}


