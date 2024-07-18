import sys


def based_on_params():
    if len(sys.argv) > 1 and sys.argv[1] == "--no-show":
        show_image = False
    else:
        show_image = True
    return show_image
