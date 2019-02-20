CREATE TABLE utilizadores(
    id INT NOT NULL AUTO_INCREMENT,
    PRIMARY KEY(id),
    user VARCHAR(50) NOT NULL UNIQUE,
    pass VARCHAR(64) NOT NULL UNIQUE
);
CREATE TABLE tarefas(
    id INT NOT NULL AUTO_INCREMENT,
    dono INT NOT NULL,
    PRIMARY KEY(id),
    FOREIGN KEY(dono) REFERENCES utilizadores(id),
    titulo VARCHAR(50) NOT NULL,
    data DATE NOT NULL,
    descrição VARCHAR(250)
);
