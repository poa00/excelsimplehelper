Чтобы проект заработал у себя надо:
1. Установить Visual studio 2017(У меня Community).
2. Установить расширение SQLite & SQL Server Compact Toolbox для Visual Studio.
3. По адресу https://system.data.sqlite.org/index.html/doc/trunk/www/downloads.wiki скачать sqlite-netFx46-setup-bundle-x86-2015-1.0.112.0.exe.
Выберите «Полная установка»
Выберите: Установить сборки в глобальный кэш сборок - Установить компоненты конструктора VS
(Галочки везде проставить)
4. Востановить пакеты NuGet, уже при открытом проекте.
5. Вместе со скаченным проектом в папке лежит файл officeWizard.db ("officewizard-master\Model\officeWizard.db),
в файле App.config можно изменить путь к файлу найдя такие строки:add name="Context" connectionString="data source=D:\OfficeMagusBD\officeWizard.db" providerName="System.Data.SQLite.EF6"
  или Создать на диске C: папку c названием OfficeMagusBD и положить туда файл с базой данных
6. В решении OfficeWizard, вместе с кодом проекта, так же лежат и файлы для работы(Без них приложение работать не будет по середине снизу будет появляться сообщение: 
"О не прописанных путях").
В проекте Model, в папке "ExternalFiles" лежат файлы, к которым надо прописать пути при включение приложения. 
Во вкладке настройки нужно выбрать файлы прожав кнопку.
--------------------------------------------------------------------------------------

Как приложение работает:
1. Убедиться, что пути к файлам во вкладке настройки есть, если нет через нажатие кнопки(Например: Файл с данными студентов, найти этот файл и щелкнуть по нему левой кнопкой мыши)
найти эти файлы.
2. Вкладка "Удостоверение/Свидетельство" 
    2.1 Написать группу можно буквами.
    2.2 Выбрать начало, конец обучения и дату выдачи документа.
    2.3 Поставить галочку, если нужна ведомость.
    2.4 Выбрать тему обучения.
    2.5 Нажать на кнопку "Создать документ".
3. Вкладка "Сертификат"
    3.1 Выбрать номер первого сертификата(все последующие будут на единицу больше(123, 124, 125)
    3.2 Написать группу можно буквами.
    3.3 Выбрать дату выдачи документа.
    3.4 Поставить галочку, если нужна ведомость.
    3.5 Выбрать тему обучения.
    3.6 Нажать на кнопку "Создать документ".
4. Вкладка "База данных"
    4.1 Вкладка "Удостоверение/Свидетельство"
    4.2 Выбери документ и нажми загрузить
    4.3 Вкладка "Сертификат"
    4.4 Выбери сертификат и нажми загрузить
    4.5 Есть поиск по параметрам
5. Вкладка "Настройки"
    5.1 Заполнить пути к файлам файлами из папки "ExternalFiles" и папкам.
    5.2 Путь результата работы => Нужно самому создать папку или выбрать любую уже готовую
    5.3 Файл с данными студентов = Файл с данными студентов
    5.4 Шаблон удостоверений = Удостоверение2
    5.5 Шаблон свидетельства = Свидетельство2
    5.7 Шаблон ведомости = Ведомость выдачи документа3
    5.8 Шаблон Сертификата = Опасные грузы
    5.9 Шаблон Сертификата 12 категории = Опасные грузы 12 категория
6. Вкладка "Программы"
    6.1 Вкладка "Программы Cвидетельств/Удосоверений"
    6.2 Заполнить поля
    6.3 Добавить программу
    6.4 Удалить программу
    6.5 Вкладка "Программы Опасные грузы"
    6.6 Заполнить поля
    6.7 Добавить программу
7. Значки в правом верхнем углу
    7.1 Левый "Свернуть"
    7.2 Правый "Закрыть"

Зачем ему работать, есть файл excel "Файл с данными студентов", нужно заполнить колонки ФИО, ДатаРождения, Оценка. Потом выбрать нужную вкладку например "Удостоверение/Свидетельство" и заполнить группа начало,конец обучения и дату выдачи, выбрать программу и нажать создать документ. Все документы создадуться с меньшим влияния человеческого фактора и меньшим временем(не надо все открывать закрывать перепроверять каждый и т.д.). Также дата выбирается(нет такого где что-то числом, а что-то прописью), а не пишется вручную, оценка цифрой и т.д.


