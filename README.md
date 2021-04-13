# BallanceTdbReader
A simple Ballance game save file reader, parser & composer.
The following is the README for the legacy version. It may not be relevant now.

## Features
1. `ByteManipulator`: A wrapper to encode/decode `Database.tdb` file, and provides APIs to easily read specific data from the file. Just calling the APIs, no manual byte manipulation required.

2. `VirtoolsArray`: You can create VirtoolsArray instances just from this class. 

## API references
### ByteManipulator
* ```C#
  byte[] Array
  ```
    Read-only property, returns the decoded byte array of this `ByteManipulator`  instance.
* ```C#
  static async Task<ByteManipulator> Create(byte[]? encoded)
  ```
  Returns a task, which creates a `ByteManipulator` instance from an encoded byte array, which is probably directly read from `Database.tdb` file.
* ```C#
  static byte[] Decode (byte[]? array)
  ```
    Takes the array in the parameter, and returns the array decoded.
* ```C#
  static byte[] Encode(byte[]? array)
  ```
    Takes the array in the parameter, and returns the array encoded.
* ```C#
  string ReadString()
  ```
    Read a string from this instance of  `ByteManipulator`, and move cursor to the end of the string.
* ```C#
  int ReadInt()
  ```
    Read the next 4 bytes as integer from this instance of `ByteManipulator`, and advancing the cursor by 4 bytes (the size of int).
* ```C#
  float ReadFloat()
  ```
    Read the next 4 bytes as float number from this instance of `ByteManipulator`, and advancing the cursor by 4 bytes (the size of float).
* ```C# 
  void Reset()
  ```
    Reset the cursor to the beginning of the internal byte array.
### VirtoolsArray

This class, as its name, is just a class for VirtoolsArray.

The correspondence of a Virtools array in file and this VirtoolsArray class is shown below.

<escape>

<table>
    <thead>
        <tr>
            <th colspan=4>string </br> SheetName</th>
        </tr>
        <tr>
            <th>Tuple&lt;string, FieldType&gt;  <br/> Headers[0]</th>
            <th>Tuple&lt;string, FieldType&gt;  <br/> Headers[1]</th>
            <th>...</th>
            <th>Tuple&lt;string, FieldType&gt;  <br/> Headers[Headers.length - 1]</th>
        </tr>
    </thead>
    <tbody>
        <tr>
          <td>object <br/> Cells[0][0]</td>
          <td>object <br/> Cells[0][1]</td>
          <td>...</td>
          <td>object <br> Cells[0][Cells[0].length - 1]</td>
        </tr>
        <tr>
          <td>object <br/> Cells[1][0]</td>
          <td>object <br/> Cells[1][1]</td>
          <td>...</td>
          <td>object <br> Cells[1][Cells[0].length - 1]</td>
        </tr>
        <tr>
          <td>...</td>
          <td>...</td>
          <td>...</td>
          <td>...</td>
        </tr>
        <tr>
          <td>object <br/> Cells[Cells.length - 1][0]</td>
          <td>object <br/> Cells[Cells.length - 1][1]</td>
          <td>...</td>
          <td>object <br> Cells[Cells.length - 1][Cells[0].length - 1]</td>
        </tr>
    </tbody>
</table>

</escape>

The following are the explanation of these properties in detail.

* ```C#
  string SheetName
  ```
    Read-only property, returns a string, indicating the `SheetName` of this VirtoolsArray instance.
  
* ```C#
  List<Tuple<string, FieldType>> Headers
  ```
  Read-only property, returns a `List` of `Tuple<string, FieldType>`, indicating the `Headers` of this VirtoolsArray instance. `Item1` of the `Tuple` is a `string`, indicating the name of this specified header, while `Item2` of the `Tuple` is a enum of `FieldType`. For more info of `FieldType`, you can refer to [here]("https://ballance.jxpxxzj.cn/wiki/Database.tdb/zh#.E8.A1.A8.E5.A4.B4.E5.88.97.E8.A1.A8").

* ```C#
  object[, ] Cells
  ```

  Returns a 2-dimentional array of `object`, referring to all of the cells in this `VirtoolsArray`.

* ```C#
  static async Task<VirtoolsArray> Create(ByteManipulator bm)
  ```

    It takes a `ByteManipulator` instance, and returns a task, which creates an `VirtoolsArray`, and reads bytes from the `ByteManipulator` and fill in the [metadata](https://ballance.jxpxxzj.cn/wiki/Database.tdb/zh#.E5.AD.98.E5.82.A8.E6.A0.BC.E5.BC.8F) for the newly created `VirtoolsArray` .

* ```C#
  async Task SetHeader(ByteManipulator bm)
  ```

  It takes a `ByteManipulator` instance, and returns a task, which reads from the    `ByteManipulator` and fill in the `Headers` for this `VirtoolsArray` instance.

* ```C#
  async Task PopulateCells(ByteManipulator bm)
  ```

  It takes a `ByteManipulator` instance, and returns a task, which reads from the    `ByteManipulator` and fill in the `Cells` for this `VirtoolsArray` instance.

* ```C#
  async Task<byte[]> ToByteArray()
  ```

  Returns this `VirtoolsArray` instance to byte array in raw form. (Not encoded, you may call `ByteManipulator.Encode` before writing it to file).

* ```C#
  static int ConvertKeyToIndex(string key)
  ```
  Convert int presented key values to human-readable key description. The conversion table can be found [here]("https://ballance.jxpxxzj.cn/wiki/Database.tdb/zh#DB_Options").

* ```C#
  static string ConvertIndexToKey(int index)
  ```
  Convert  human-readable key description to int presented key values.
