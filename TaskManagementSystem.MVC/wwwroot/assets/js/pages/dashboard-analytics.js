/**
* Theme: Attex - Responsive Tailwind CSS 3 Admin Dashboard
* Author: Coderthemes
* Module/App: Dashboard Analytics
*/


window.Apex = {
    chart: {
        parentHeightOffset: 0,
        toolbar: {
            show: false
        }
    },
    grid: {
        padding: {
            left: 0,
            right: 0
        }
    },
    colors: ["#16a7e9", "#47ad77", "#fa5c7c", "#ffbc00"],
};

function getRandomData(number) {
    var graphData = [];
    for (var idx = 0; idx < number; idx++) {
        graphData.push(Math.floor(Math.random() * Math.floor(90)) + 30);
    }
    return graphData;
}

function getDaysInMonth(month, year) {
    var date = new Date(year, month, 1);
    var days = [];
    var idx = 0;
    while (date.getMonth() === month && idx < 15) {
        var d = new Date(date);
        days.push(d.getDate() + " " + d.toLocaleString('en-us', { month: 'short' }));
        date.setDate(date.getDate() + 1);
        idx += 1;
    }
    return days;
}

var now = new Date();
var labels = getDaysInMonth(now.getMonth() + 1, now.getFullYear());

var colors = ["#16a7e9", "#47ad77", "#fa5c7c", "#ffbc00"];
var dataColors = document.querySelector("#sessions-overview").dataset.colors;
if (dataColors) {
    colors = dataColors.split(",");
}
var options = {
    chart: {
        height: 240,
        type: 'area'
    },
    dataLabels: {
        enabled: false
    },
    stroke: {
        curve: 'smooth',
        width: 4
    },
    series: [{
        name: 'Sessions',
        data: [10, 20, 5, 15, 10, 20, 15, 25, 20, 30, 25, 40, 30, 50, 35]
    }],
    zoom: {
        enabled: false
    },
    legend: {
        show: false
    },
    colors: colors,
    xaxis: {
        type: 'string',
        categories: labels,
        tooltip: {
            enabled: false
        },
        axisBorder: {
            show: false
        },
        labels: {

        }
    },
    yaxis: {
        labels: {
            formatter: function (val) {
                return val + "k"
            },
            offsetX: -15
        }
    },
    fill: {
        type: 'gradient',
        gradient: {
            type: "vertical",
            shadeIntensity: 1,
            inverseColors: false,
            opacityFrom: 0.45,
            opacityTo: 0.05,
            stops: [45, 100]
        },
    },
}

var chart = new ApexCharts(
    document.querySelector("#sessions-overview"),
    options
);

chart.render();

// --------------------------------------------------
var categories = [];
for (var i = 10; i >= 1; i--) {
    categories.push(i + ' min ago');
}
function getRandomData(length) {
    var d = [];
    for (var idx = 0; idx < length; idx++) {
        d.push(Math.floor(Math.random() * 90) + 10);
    }
    return d;
}

var colors = ["#16a7e9", "#47ad77", "#fa5c7c", "#ffbc00"];
var dataColors = document.querySelector("#views-min").dataset.colors;
if (dataColors) {
    colors = dataColors.split(",");
}
var options = {
    chart: {
        height: 216,
        type: 'bar',
        stacked: true
    },
    plotOptions: {
        bar: {
            horizontal: false,
            endingShape: "rounded",
            columnWidth: "22%",
            dataLabels: {
                position: 'top', // top, center, bottom
            },
        },
    },
    dataLabels: {
        enabled: true,
        offsetY: -24,
        style: {
            fontSize: '12px',
            colors: ["#8a969c"]
        }
    },
    series: [{
        name: 'Views',
        data: getRandomData(10)
    }],
    zoom: {
        enabled: false
    },
    legend: {
        show: false
    },
    colors: colors,
    xaxis: {
        categories: categories,
        labels: {
            show: false
        },
        axisTicks: {
            show: false
        },
        axisBorder: {
            show: false
        }
    },
    yaxis: {
        labels: {
            show: false
        }
    },
    fill: {
        type: "gradient",
        gradient: {
            inverseColors: !0,
            shade: "light",
            type: "horizontal",
            shadeIntensity: .25,
            gradientToColors: void 0,
            opacityFrom: 1,
            opacityTo: 1,
            stops: [0, 100, 100, 100]
        }
    },
    tooltip: {
        y: {
            formatter: function (val) {
                return val;
            }
        },
    },
}

var chart2 = new ApexCharts(
    document.querySelector("#views-min"),
    options
);

chart2.render();


// ------------ sessions by browser
var colors = ["#16a7e9", "#47ad77", "#fa5c7c", "#ffbc00"];
var dataColors = document.querySelector("#sessions-browser").dataset.colors;
if (dataColors) {
    colors = dataColors.split(",");
}
var options = {
    chart: {
        height: 364,
        type: 'radar',
    },
    series: [{
        name: 'Usage',
        data: [80, 50, 30, 40, 60, 20],
    }],
    labels: ['Chrome', 'Firefox', 'Safari', 'Opera', 'Edge', 'Explorer'],
    plotOptions: {
        radar: {
            size: 130,
            polygons: {
                strokeColor: '#e9e9e9',
                fill: {
                    colors: ['#f8f8f8', '#fff']
                }
            }
        }
    },
    colors: colors,
    yaxis: {
        labels: {
            formatter: function (val) {
                return val + "%";
            }
        },
    },
    dataLabels: {
        enabled: true
    },
    markers: {
        size: 4,
        colors: ['#fff'],
        strokeColor: colors[0],
        strokeWidth: 2,
    }
}

var chart = new ApexCharts(
    document.querySelector("#sessions-browser"),
    options
);

chart.render();

/* ------------- visitors by os */
var colors = ["#16a7e9", "#47ad77", "#fa5c7c", "#ffbc00"];
var dataColors = document.querySelector("#sessions-os").dataset.colors;
if (dataColors) {
    colors = dataColors.split(",");
}
var options = {
    chart: {
        height: 315,
        type: 'radialBar',
    },
    plotOptions: {
        radialBar: {
            dataLabels: {
                name: {
                    fontSize: '22px',
                },
                value: {
                    fontSize: '16px',
                },
                total: {
                    show: true,
                    label: 'OS',
                    formatter: function (w) {
                        // By default this function returns the average of all series. The below is just an example to show the use of custom formatter function
                        return 8541
                    }
                }
            }
        }
    },
    colors: colors,
    series: [44, 55, 67, 83],
    labels: ['Windows', 'Macintosh', 'Linux', 'Android']
}

var chart = new ApexCharts(
    document.querySelector("#sessions-os"),
    options
);

chart.render();

//
// Campaign Sent Chart
//
var colors = ["#16a7e9"];
var dataColors = document.querySelector("#campaign-sent-chart").dataset.colors;
if (dataColors) {
    colors = dataColors.split(",");
}
var options1 = {
    chart: {
        type: 'bar',
        height: 60,
        sparkline: {
            enabled: true
        }
    },
    plotOptions: {
        bar: {
            columnWidth: '60%'
        }
    },
    colors: colors,
    series: [{
        data: [25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54]
    }],
    labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11],
    xaxis: {
        crosshairs: {
            width: 1
        },
    },
    tooltip: {
        fixed: {
            enabled: false
        },
        x: {
            show: false
        },
        y: {
            title: {
                formatter: function (seriesName) {
                    return ''
                }
            }
        },
        marker: {
            show: false
        }
    }
}

new ApexCharts(document.querySelector("#campaign-sent-chart"), options1).render();

// 
// New Leads Chart
//
var colors = ["#16a7e9"];
var dataColors = document.querySelector("#new-leads-chart").dataset.colors;
if (dataColors) {
    colors = dataColors.split(",");
}
var options2 = {
    chart: {
        type: 'line',
        height: 60,
        sparkline: {
            enabled: true
        }
    },
    series: [{
        data: [25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54]
    }],
    stroke: {
        width: 2,
        curve: 'smooth'
    },
    markers: {
        size: 0
    },
    colors: colors,
    tooltip: {
        fixed: {
            enabled: false
        },
        x: {
            show: false
        },
        y: {
            title: {
                formatter: function (seriesName) {
                    return ''
                }
            }
        },
        marker: {
            show: false
        }
    }
}


new ApexCharts(document.querySelector("#new-leads-chart"), options2).render();


// active counts
setInterval(function () {
    var ac = Math.floor(Math.random() * 600 + 150);
    document.getElementById("active-users-count").textContent = ac;
    document.getElementById("active-views-count").textContent = Math.floor(Math.random() * ac + 200);
}, 2000);


/* ------------- visitors by country */
var colors = ["#3e60d5", "#47ad77", "#fa5c7c", "#ffbc00"];
var dataColors = document.querySelector("#country-chart").dataset.colors;
if (dataColors) {
    colors = dataColors.split(",");
}
var options = {
    chart: {
        height: 390,
        type: 'bar',
    },
    plotOptions: {
        bar: {
            horizontal: true,
        }
    },
    colors: colors,
    dataLabels: {
        enabled: false
    },
    series: [{
        name: 'Orders',
        data: [90, 75, 60, 50, 45, 36, 28, 20, 15, 12]
    }],
    xaxis: {
        categories: ["India", "China", "United States", "Japan", "France", "Italy", "Netherlands", "United Kingdom", "Canada", "South Korea"],
        axisBorder: {
            show: false,
        },
        labels: {
            formatter: function (val) {
                return val + "%";
            }
        }
    },
    grid: {
        strokeDashArray: [5]
    }
}

var chart = new ApexCharts(
    document.querySelector("#country-chart"),
    options
);

chart.render();


// World Map
const map = new jsVectorMap({
    map: 'world',
    selector: '#world-map-markers',
    zoomOnScroll: false, 
    zoomButtons: true,
    markersSelectable: true,
    markers: [
        { name: "Greenland", coords: [72, -42] },
        { name: "Canada", coords: [56.1304, -106.3468] },
        { name: "Brazil", coords: [-14.2350, -51.9253] },
        { name: "Egypt", coords: [26.8206, 30.8025] },
        { name: "Russia", coords: [61, 105] },
        { name: "China", coords: [35.8617, 104.1954] },
        { name: "United States", coords: [37.0902, -95.7129] },
        { name: "Norway", coords: [60.472024, 8.468946] },
        { name: "Ukraine", coords: [48.379433, 31.16558] },
    ],
    markerStyle: {
        initial: { fill: "#3e60d5" },
        selected: { fill: "#3e60d56e" }
    },
    labels: {
        markers: {
            render: marker => marker.name
        }
    }
});