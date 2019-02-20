<?php
require_once "connection.php";

$con = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

$tarefaID = $_POST["tarefaID"];

    $statement = "DELETE FROM tarefas WHERE id = '$tarefaID'";
    if (mysqli_query($con,$statement))
    {
        echo "0";
    }
    else
    {
        echo "1";
    }
?>
