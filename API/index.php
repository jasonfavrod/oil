<?php
require('PriceData.php');

$get = [];

if ($_REQUEST) {
    $get = filter_input_array(INPUT_GET, [
        '/price' => FILTER_UNSAFE_RAW
    ]);
}


if (array_key_exists('/price', $get)) {
    header('Content-Type: application/json');
    print PriceData::getAll();
}
