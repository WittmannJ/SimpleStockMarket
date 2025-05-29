function getRandomColor() {
    let hue = Math.floor(Math.random() * 360); // Random hue (0-360)
    let saturation = 70 + Math.random() * 30; // Keep saturation high (70-100%)
    let lightness = 50 + Math.random() * 20; // Keep lightness balanced (50-70%)

    return `hsl(${hue}, ${saturation}%, ${lightness}%)`;
}

function renderStockChart(chartId, labels, prices, symbol) {
    var ctx = document.getElementById(chartId).getContext('2d');
    var randomColor = getRandomColor();

    new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: symbol + " Stock Price",
                data: prices,
                borderColor: randomColor,
                backgroundColor: randomColor.replace(")", ", 0.2)"), // Transparent fill
                fill: true,
                pointRadius: 0 // Removes small circles for readability
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false
        }
    });
}