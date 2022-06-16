const express = require('express');
const app = express();
const port = process.env.NODE_ENV === 'test' ? 3001 : 3000;
const dronesRouter = require('./routes/drones');

app.get('/', (req, res) => {
    res.json({ message: 'alive' });
});

app.use(express.json());

app.use('/drones', dronesRouter);

app.listen(port, () => {
    console.log(`Example app listening at http://localhost:${port}`);
});