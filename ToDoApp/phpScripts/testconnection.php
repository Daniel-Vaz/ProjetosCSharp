<?php
    require_once "connection.php";
    $conn = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_DATABASE);

    if(!$conn) {
      die ("Connection failed: " . mysqli_connect_error());
    }

    else {
      echo "Connected successfully";
    }
?>
