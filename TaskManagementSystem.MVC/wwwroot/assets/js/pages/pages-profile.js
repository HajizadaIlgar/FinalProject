/**
 * Theme: Attex - Responsive Tailwind CSS 3 Admin Dashboard
 * Author: Coderthemes
 * Module/App: Profile
 */

(function () {
    "use strict";

    var Profile = function () {
        this.body = document.querySelector("body");
        this.charts = [];
    };

    Profile.prototype.respChart = function (selector, type, data, options) {
        var draw3 = Chart.controllers.bar.prototype.draw;
        Chart.controllers.bar.draw = function () {
            draw3.apply(this, arguments);
            var ctx = this.chart.ctx;
            var _fill = ctx.fill;
            ctx.fill = function () {
                ctx.save();
                ctx.shadowColor = 'rgba(0,0,0,0.01)';
                ctx.shadowBlur = 5;
                ctx.shadowOffsetX = 4;
                ctx.shadowOffsetY = 5;
                _fill.apply(this, arguments);
                ctx.restore();
            };
        };

        //default config
        Chart.defaults.font.color = "rgba(93,106,120,0.2)";
        Chart.defaults.scale.grid.color = "rgba(93,106,120,0.2)";

        var ctx = selector.getContext("2d");
        var container = selector.parentNode;

        function generateChart() {
            var ww = selector.setAttribute('width', container.offsetWidth);
            var chart;
            switch (type) {
                case 'Line':
                    chart = new Chart(ctx, { type: 'line', data: data, options: options });
                    break;
                case 'Doughnut':
                    chart = new Chart(ctx, { type: 'doughnut', data: data, options: options });
                    break;
                case 'Pie':
                    chart = new Chart(ctx, { type: 'pie', data: data, options: options });
                    break;
                case 'Bar':
                    chart = new Chart(ctx, { type: 'bar', data: data, options: options });
                    break;
                case 'Radar':
                    chart = new Chart(ctx, { type: 'radar', data: data, options: options });
                    break;
                case 'PolarArea':
                    chart = new Chart(ctx, { data: data, type: 'polarArea', options: options });
                    break;
            }
            return chart;
        }

        return generateChart();
    };

    Profile.prototype.initCharts = function () {
        var charts = [];

        var highPerformingProduct = document.getElementById('high-performing-product');
        if (highPerformingProduct) {
            var ctx = highPerformingProduct.getContext("2d");
            var gradientStroke = ctx.createLinearGradient(0, 500, 0, 150);
            gradientStroke.addColorStop(0, "#409c6b");
            gradientStroke.addColorStop(1, "#3e60d5");

            var barChart = {
                labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
                datasets: [
                    {
                        label: "Orders",
                        backgroundColor: gradientStroke,
                        borderColor: gradientStroke,
                        hoverBackgroundColor: gradientStroke,
                        hoverBorderColor: gradientStroke,
                        data: [65, 59, 80, 81, 56, 89, 40, 32, 65, 59, 80, 81]
                    },
                    {
                        label: "Revenue",
                        backgroundColor: "rgba(93,106,120,0.2)",
                        borderColor: "rgba(93,106,120,0.2)",
                        hoverBackgroundColor: "rgba(93,106,120,0.2)",
                        hoverBorderColor: "rgba(93,106,120,0.2)",
                        data: [89, 40, 32, 65, 59, 80, 81, 56, 89, 40, 65, 59]
                    }
                ]
            };

            var barOpts = {
                maintainAspectRatio: false,
                datasets: {
                    bar: {
                        barPercentage: 0.7,
                        categoryPercentage: 0.5
                    }
                },
                plugins: {
                    legend: {
                        display: false
                    }
                },
                scales: {
                    y: {
                        grid: {
                            display: false,
                            color: "rgba(0,0,0,0.05)"
                        },
                        stacked: false,
                        ticks: {
                            stepSize: 20
                        }
                    },
                    x: {
                        stacked: false,
                        grid: {
                            color: "rgba(0,0,0,0.01)"
                        }
                    }
                }
            };

            charts.push(this.respChart(highPerformingProduct, 'Bar', barChart, barOpts));
        }

        return charts;
    };

    Profile.prototype.init = function () {
        var self = this;
        Chart.defaults.font.family = '-apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,Oxygen-Sans,Ubuntu,Cantarell,"Helvetica Neue",sans-serif';

        self.charts = this.initCharts();

        window.addEventListener('resize', function (e) {
            self.charts.forEach(function (chart) {
                try {
                    chart.destroy();
                } catch (err) { }
            });
            self.charts = self.initCharts();
        });
    };

    var profile = new Profile();
    profile.init();
})();
