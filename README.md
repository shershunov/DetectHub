<p align="center">
  <a href='https://github.com/EkelviNistars/DetectHub/releases'>
  <img src="https://i.imgur.com/8DvMAK4.png" alt="DetectHub"/>
  </a>
  <br>
  <br>
  DetectHub - Программа для обнаружения объектов в реальном времени на основе моделей <a href='https://github.com/ultralytics/ultralytics'>YoloV8</a> и <a href='https://github.com/ultralytics/yolov5'>YoloV5</a> в формате .onnx
  <br>
  <br>
</p>

# Экспорт через PyTorch
Код для конвертации своей модели Yolo из .pt в .onnx

```python
from ultralytics import YOLO

# Загрузка модели
model = YOLO('path/to/model')

# Экспорт в .onnx формат
model.export(format='onnx', opset=15)
```

# Превью
<p align="center">
  <img src="https://i.imgur.com/L0poGqP.png" alt="Preview" width="85%"/>
</p>
