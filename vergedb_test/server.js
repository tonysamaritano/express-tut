const app = require('./app.js');
const port = process.env.NODE_ENV === 'test' ? 3001 : 3000;

app.listen(port, () => {
    console.log(`Example app listening at http://localhost:${port}`);
});

module.exports = {
    app
}
