from diagrams import Diagram
from architecture_diagrams.parcel_service_with_outbox import parcel_service_with_outbox_diagram
from tools.DiagramTools import based_on_params

show_image = based_on_params()

with Diagram(show=show_image, direction="LR", filename="parcel_service_with_outbox", outformat="png"):
    parcel_service_with_outbox_diagram()