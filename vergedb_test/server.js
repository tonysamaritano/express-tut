const express = require('express');
const app = express();
const port = 3000 || process.env.PORT;
const dronesRouter = require('./routes/drones');
const singleRouter = require('./routes/single');

app.get('/', (req, res) => {
    res.json({ message: 'alive' });
});

app.use(express.json());

app.use('/drones', dronesRouter);

app.listen(port, () => {
    console.log(`Example app listening at http://localhost:${port}`);
});