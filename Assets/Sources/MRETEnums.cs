namespace Sources {
    public enum SceneType {
        Base,
        SplitScreen
    }

    public enum AnimalType {
        Spider,
        Snake,
        Rat
    }

    public static class SceneNames {
        public const string Base = "Main";
        public const string Split = "MainSplit";
    }
        
    public enum SpawningType {
        Single,
        Multiple
    }

    public enum ApplicationMode {
        Development,
        Release
    }
}