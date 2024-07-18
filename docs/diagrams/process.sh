echo "Running all *.py files."
for file in ./*.py; do 
    echo "    $file";
    python3 "$file" --no-show; 
done

echo "Done"
