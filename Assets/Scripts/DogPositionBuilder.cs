namespace Assets.Scripts {
    public static class DogPositionBuilder {

        private static int _dogPosition;

        public static int GetNext(){
            _dogPosition += 30;
            return _dogPosition;
        }
    }
}
