## Общая структура проекта для домашнего задания по параллельной загрузке данных

### Постановка задачи ###

Цель: Сделать параллельный обработчик файла с данными клиентов на основе подготовленного проекта с архитектурой. 
Задание поможет отработать основные инструменты параллелизма на реалистичной задаче.
Каждая строка файла содержит id (целое число), ФИО (строка), email (строка), телефон (строка). Данные отсортированы по id. Нужно десериализовать данные клиента в объект и передать объект в метод класса, который сохраняет его в БД, вместо сохранения в БД сделать просто задержку.

Сделать несколько пунктов задания: 
1. Запуск генератора файла через создание процесса, сделать возможность выбора в коде, как запускать генератор, процессом или через вызов метода. Если вдруг встретится баг с генерацией, то его нужно исправить и написать об этом.
2. Распараллеливаем обработку файла по набору диапазонов Id, то есть нужно, чтобы файл разбивался на диапазоны по Id и обрабатывался параллельно через Thread или Task. Предусмотреть обработку ошибок в обработчике и перезапуск по ошибке с указанием числа попыток. Проверить обработку на файле, в котором 1 млн. записей
3*. Реализовать ту же задачу на Task, если было сделано через Thread и через Task, если было сделано через Thread. Написать свое мнение удобнее ли работать с Task или с Thread.
4*. Добавить сохранение в реальную БД, можно SQL Lite для простоты тестирования
5*. Сделать обработку файла в CSV формате, то есть написать генератор и разбор файла

### Инструкция ###
1. Скопировать в свой репозиторий шаблон, если необходимо, то можно изменить структуру проектов, классов и интерфейсов, как считаете нужным
https://bitbucket.org/avgrankovskiy/otus.teaching.concurrency.import/src/master/
2. Реализовать 1 пункт задания, сделав в main проекта запуск процесса-генератора файла, его нужно будет собрать отдельно и передать в программу путь к .exe файлу, также сделать в main вызов кода генератора из подключенного проекта, выбор между процессом или вызовом метода сделать настройкой со значением по умолчанию для метода.
3. Реализовать 2 пункт задания, сделав свои реализации для IDataLoader и IDataParser
4. По желанию реализовать 3 пункт задания, сделав дополнительную реализацию IDataLoader.
5. По желанию реализовать 4 пункт задания, сделав дополнительную реализацию для ICustomerRepository и инициализацию БД при старте приложения, можно использовать EF
6. По желанию реализовать 5 пункт задания, сделав дополнительную реализацию для IDataParser и IDataGenerator

### Оценка ### 
Всего 10 баллов:
1. 1 балл
2. 4 балла
3. 2 балла
4. 1 балл
5. 2 балла

Для приема задания достаточно 7 баллов

### Otus.Teaching.Concurrency.Import.Loader

Консольное приложение, которое должно сгенирировать файл с данными и запустить загрузку из него через реализацию `IDataLoader`.

### Otus.Teaching.Concurrency.Import.DataGenerator

Библиотека, в которой должна определена логика генерации файла с данными, в базовом варианте это XML.

### Otus.Teaching.Concurrency.Import.DataGenerator.App

Консольное приложение, которое позволяет при запуске отдельно выполнить генерацию файла из `DataGenerator` библиотеки.

### Otus.Teaching.Concurrency.Import.DataAccess

Библиотека, в которой находится доступ к базе данных и файлу с данными.Сделать обработку если файл будет в CSV формате, то есть написать генератор и разбор файла.

### Otus.Teaching.Concurrency.Import.Core

Библиотека, в которой определены сущности БД и основные интерфейсы, которые реализуют другие компоненты.