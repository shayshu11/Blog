﻿var margin = { top: 20, right: 20, bottom: 30, left: 40 },
width = 480 - margin.left - margin.right,
height = 250 - margin.top - margin.bottom;

var x0 = d3.scale.ordinal()
    .rangeRoundBands([0, width], .1);

var x1 = d3.scale.ordinal();

var y = d3.scale.linear()
    .range([height, 0]);

var color = d3.scale.ordinal()
    .range(["#e91e63", "#2196f3"]);

var xAxis = d3.svg.axis()
    .scale(x0)
    .orient("bottom");

var yAxis = d3.svg.axis()
    .scale(y)
    .orient("left")
    .tickFormat(d3.format("d"));

var svg = d3.select(".graphDiv").append("svg")
    .attr("width", width + margin.left + margin.right)
    .attr("height", height + margin.top + margin.bottom)
  .append("g")
    .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

d3.json("/Fans/GetRegisterStats", function (error, data) {
    if (error) throw error;

    //var genderNames = d3.keys(Object.keys(data[0])).filter(function (key) { return key !== "month"; });
    var genderNames = Object.keys(data[0]).filter(function (key) { return key !== "month"; });

    data.forEach(function (d) {
        d.genders = genderNames.map(function (name) { return { name: name, value: +d[name] }; });
    });

    x0.domain(data.map(function (d) { return d.month; }));
    x1.domain(genderNames).rangeRoundBands([0, x0.rangeBand()]);
    y.domain([0, d3.max(data, function (d) { return d3.max(d.genders, function (d) { return d.value; }); }) + 3]);

    svg.append("g")
      .attr("class", "x axis")
      .attr("transform", "translate(0," + height + ")")
      .call(xAxis);

    svg.append("g")
        .attr("class", "y axis")
        .call(yAxis)
      .append("text")
        .attr("transform", "rotate(-90)")
        .attr("y", 6)
        .attr("dy", ".71em")
        .style("text-anchor", "end")
        .text("Registrations");

    var month = svg.selectAll(".month")
      .data(data)
    .enter().append("g")
      .attr("class", "month")
      .attr("transform", function (d) { return "translate(" + x0(d.month) + ",0)"; });

    month.selectAll("rect")
      .data(function (d) { return d.genders; })
    .enter().append("rect")
      .attr("width", x1.rangeBand())
      .attr("x", function (d) { return x1(d.name); })
      .attr("y", function (d) { return y(d.value); })
      .attr("height", function (d) { return height - y(d.value); })
      .style("fill", function (d) { return color(d.name); });

    var legend = svg.selectAll(".legend")
      .data(genderNames.slice().reverse())
    .enter().append("g")
      .attr("class", "legend")
      .attr("transform", function (d, i) { return "translate(0," + i * 20 + ")"; });

    legend.append("rect")
        .attr("x", width - 10)
        .attr("width", 10)
        .attr("height", 10)
        .style("fill", color);

    legend.append("text")
        .attr("x", width - 24)
        .attr("y", 9)
        .attr("dy", ".35em")
        .style("text-anchor", "end")
        .text(function (d) { return d; });
});