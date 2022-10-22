{ pkgs ? import (fetchTarball "https://github.com/NixOS/nixpkgs/archive/7ad22ae49d66eb16eec6b97f9eaca9399dddbabf.tar.gz") {} }:

pkgs.mkShell {
  buildInputs = with pkgs; [
    dotnet-sdk
	  dotnetPackages.Nuget
  ];
}

