<?php
require_once "connection.php";

$con = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

    $username = $_POST["username"];
    $password = $_POST["password"];

    $check="SELECT * FROM utilizadores WHERE user = '$username'";
    $rs = mysqli_query($con,$check);
    $data = mysqli_fetch_array($rs, MYSQLI_NUM);

    if($data[0] > 1) {
        echo "1";
    }

    else
    {
        $statement = "INSERT INTO utilizadores (user, pass) VALUES ('$username', '$password')";
        if (mysqli_query($con,$statement))
        {
            echo "0";
        }
        else
        {
            echo "2";
        }
    }

?>
