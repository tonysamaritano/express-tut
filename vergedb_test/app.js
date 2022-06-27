const express = require('express');
const app = express();
const dronesRouter = require('./routes/drones');

console.log('In apps.js');

app.use(express.json());

app.use('/drones', dronesRouter);

app.get('/', (req, res) => {
  res.json({ message: 'alive' });
});

app.use((req, res, next) => {
    const error = new Error('Not found');
    error.statusCode = 404;
    next(error);
  });
  
app.use((err, req, res, next) => {
    const statusCode = err.statusCode || 500;
    const name = err.name || 'Error';
    res
    .status(statusCode)
    .json({ name, message: err.message });
});

module.exports = app