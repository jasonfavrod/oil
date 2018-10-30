<?php
require 'conf/db_config.php';

class PriceData
{
    public static function getAll()
    {
        $res = null;

        try {
            $conn = new PDO(DSN);
            $res = $conn->query("select * from price_data.oil;");
            $res->setFetchMode(PDO::FETCH_ASSOC);
            $res = json_encode($res->fetchAll(), JSON_UNESCAPED_SLASHES);
        }
        catch (PDOException $e) {
            $res = json_encode([ (object)['error' => $e->getMessage()] ]);
        }
        finally {
            if ($res) return $res;
            else return json_encode([]);
        }
    }
}
