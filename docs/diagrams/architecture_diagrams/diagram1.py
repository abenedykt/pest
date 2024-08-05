from diagrams.aws.compute import ECS


def diag1():
    puid = ECS("PUID Service")
    hello = ECS("Hello Service")
    rest = ECS("REST API")
    whartever = ECS("Whatever Service")

    puid >> rest >> [hello, whartever] >> puid


    return puid, rest, hello