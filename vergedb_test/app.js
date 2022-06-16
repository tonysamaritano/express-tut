const express = require('express');
const app = express();
const dronesRouter = require('./routes/drones');

console.log('In apps.js');

app.use(express.json());

app.use('/drones', dronesRouter);

module.exports = app