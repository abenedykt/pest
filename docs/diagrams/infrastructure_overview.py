from diagrams import Diagram
from architecture_diagrams.infrastructure_overview import infrastructure_overview_diagram
from tools.DiagramTools import based_on_params

show_image = based_on_params()

with Diagram(show=show_image, direction="TB", filename="infrastructure_overview", outformat="png"):
   infrastructure_overview_diagram()