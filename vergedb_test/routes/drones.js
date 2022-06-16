const express = require('express');
const router = express.Router();
const drones = require('../services/drones');

console.log('In drone router');

/* GET drones listing. */
router.get('/', function (req, res, next) {
    try {
        console.log('In wrong get');
        //Commands here
        res.json(drones.getMultiple(req.query.page));
        
    } catch (err) {
        console.error(`Error while getting drones `, err.message);
        next(err);
    }
});

router.get('/:id', function (req, res) {
    console.log('In correct get');
    res.json(drones.getSingle(req.params.id));
});


/* DELETE drones */
router.delete('/:id', function (req, res) {
    res.json(drones.removeSingle(req.params.id));
});

/*PUT drones*/
router.put('/:id', function (req, res, next) {
    try {
        res.json(drones.putSingle(req.body, req.params.id));
    } catch (err) {
        console.error(`Error while updating drones `, err.message);
        next(err);
    }
});

/* PATCH drones */
router.patch('/:id', function (req, res) {
    res.json(drones.patchSingle(req.body, req.params.id));
});

/* POST drones */
router.post('/', function (req, res, next) {
    try {
        //Commands here
        res.json(drones.create(req.body));
    } catch (err) {
        console.error(`Error while adding drones `, err.message);
        next(err);
    }
});

module.exports = router;