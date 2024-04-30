<p align="center">
  <a href='https://github.com/EkelviNistars/DetectHub/releases'>
  <img src="https://i.imgur.com/8DvMAK4.png" alt="DetectHub" width="85%"/>
  </a>
  <br>
  <br>
  DetectHub - Real-time object detection program based on YoloV8 and YoloV5 models in .onnx format.
<br>
<br>
</p>

# Export via PyTorch
Code to convert your Yolo model from .pt to .onnx

```python
from ultralytics import YOLO

model = YOLO('path/to/model')

model.export(format='onnx', opset=15)
```

# Preview
<p align="center"><a href='https://github.com/EkelviNistars/DetectHub/releases'>
  <img src="https://i.imgur.com/8lIN5YY.png" alt="Preview" width="85%"/></a>
</p>
